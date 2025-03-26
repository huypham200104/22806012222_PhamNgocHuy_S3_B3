using _22806012222_PhamNgocHuy_S3_B3.Data;
using _22806012222_PhamNgocHuy_S3_B3.Models;
using _22806012222_PhamNgocHuy_S3_B3.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _22806012222_PhamNgocHuy_S3_B3.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cartItems = await _context.OrderDetails
                .Where(od => od.UserId == user.Id && od.OrderId == null) // Chỉ lấy của người dùng hiện tại
                .Include(od => od.Product)
                .ToListAsync();

            return View(cartItems);
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await GetProductFromDatabase(productId);
            var user = await _userManager.GetUserAsync(User);

            var existingItem = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.UserId == user.Id && od.ProductId == productId && od.OrderId == null);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.Price = product.Price;
            }
            else
            {
                var cartItem = new OrderDetail
                {
                    UserId = user.Id,  // Gán UserId cho mục giỏ hàng
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price,
                    OrderId = null
                };
                _context.OrderDetails.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            var item = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.UserId == user.Id && od.ProductId == productId && od.OrderId == null);

            if (item != null)
            {
                _context.OrderDetails.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var user = await _userManager.GetUserAsync(User);
            var cartItems = await _context.OrderDetails
                .Where(od => od.UserId == user.Id && od.OrderId == null)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }

            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cartItems.Sum(i => i.Price * i.Quantity);
            order.OrderDetails = cartItems;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in order.OrderDetails)
            {
                item.OrderId = order.Id;
            }
            await _context.SaveChangesAsync();

            return View("OrderCompleted", order.Id);
        }

        private async Task<Product> GetProductFromDatabase(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }
    }
}
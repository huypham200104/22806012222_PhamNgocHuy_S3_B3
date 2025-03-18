using _22806012222_PhamNgocHuy_S3_B3.Models;
using _22806012222_PhamNgocHuy_S3_B3.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _22806012222_PhamNgocHuy_S3_B3.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        // 
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Product model, IFormFile? ImageUrl)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
                return View(model);
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Nếu có ảnh mới, lưu vào wwwroot/images
            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{ImageUrl.FileName}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn ảnh mới
                product.ImageUrl = "/images/" + fileName;
            }

            // Cập nhật thông tin sản phẩm
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;

            await _productRepository.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> FilterByPrice(decimal maxPrice)
        {
            var products = await _productRepository.GetAllAsync();
            var filteredProducts = products.Where(p => p.Price <= maxPrice).ToList();
            return View("Index", filteredProducts);
        }

    }


}
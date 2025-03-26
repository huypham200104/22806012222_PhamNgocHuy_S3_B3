using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _22806012222_PhamNgocHuy_S3_B3.Data; // Thay đổi namespace theo DbContext của bạn
using _22806012222_PhamNgocHuy_S3_B3.Models;
using Microsoft.AspNetCore.Authorization; // Namespace cho model Order

namespace _22806012222_PhamNgocHuy_S3_B3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context; // Thay ApplicationDbContext bằng tên DbContext của bạn

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync(); // Lấy tất cả đơn hàng
            return View(orders);
        }
    }
}
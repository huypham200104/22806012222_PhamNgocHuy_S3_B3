using Microsoft.AspNetCore.Mvc;
using _22806012222_PhamNgocHuy_S3_B3.Models;
using _22806012222_PhamNgocHuy_S3_B3.Repository;
using System.Diagnostics;
using _22806012222_PhamNgocHuy_S3_B3.Data;

namespace _22806012222_PhamNgocHuy_S3_B3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public IActionResult Index()
    {
        var products = _context.Products.ToList(); // Lấy danh sách sản phẩm từ database
        return View(products);
    }

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

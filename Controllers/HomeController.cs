using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models;
using System.Diagnostics;
using Pharmacy.Data;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var BestProducts = await _context.Products.OrderBy(x => x.Price).Take(6).ToListAsync();
            var NewProducts = await _context.Products.OrderBy(x => x.CreatedAt).Take(4).ToListAsync();
            return View(NewProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
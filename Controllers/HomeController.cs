using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Domain;

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

        public async Task<IActionResult> Index() { 
            var products = await _context.Products.OrderByDescending(x => x.CreatedAt).Take(4).ToListAsync();
            var BestProducts = await _context.Products.OrderByDescending(x => x.Price).Take(6).ToListAsync();
            foreach(var product in BestProducts) products.Add(product);
            return View(products);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AdminPanel()
        {
            return View();
        }

    }
}
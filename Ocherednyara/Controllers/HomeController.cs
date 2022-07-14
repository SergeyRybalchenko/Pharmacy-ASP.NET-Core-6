using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context, IProductService productService)
        {
            _logger = logger;
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetHomeStoreProductViewModelList();
            return View(products);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AdminPanel()
        {
            return View();
        }

    }
}
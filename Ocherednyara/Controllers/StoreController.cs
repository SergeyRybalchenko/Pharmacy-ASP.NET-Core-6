using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Entities;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPagerService _pagerService;

        public StoreController(IProductService productService, IPagerService pagerService)
        {
            _productService = productService;
            _pagerService = pagerService;
        }

        /// <summary>
        /// Returns info about all products or about found products
        /// </summary>
        /// <param name="SearchString">Product search string</param>
        /// <param name="SortType">Sort type</param>
        /// <param name="PageNumber">Number of page</param>
        /// <returns>List of ProductViewModels</returns>
        public async Task<IActionResult> Index(string SearchString, string SortType, int PageNumber = 1)
        {
            var Products = await _productService.GetStoreProductViewModel(SortType, SearchString);
            var Pager = _pagerService.GetPagerViewModel(PageNumber, Products);
            Products = _pagerService.SkipProducts(Pager, Products, PageNumber);

            ViewBag.Pager = Pager;

            return View(Products);
        }

        /// <summary>
        /// Returns info about product by id
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>Detail product info</returns>
        public async Task<IActionResult> Details(Guid id)
        {
            var Product = await _productService.GetStoreSingleProductViewModel(id);
            return Product == null ? NotFound() : View(Product);
        }

        public async Task<IActionResult> CreateOrder(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([Bind("ProductId,Name,Description,Price,ImagePath,Count,CreatedAt")] Order order)
        {
            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.Now;
            return RedirectToAction(nameof(Index));
        }
    }
}

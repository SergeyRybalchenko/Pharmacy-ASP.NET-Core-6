using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy.Models;
using Pharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Service;
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
    }
}

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

        public async Task<IActionResult> Index(string SearchString, string SortType, int PageNumber = 1)
        { 
            var Products = _productService.GetStoreProductViewModel(SortType, SearchString);
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
            var Product = _productService.GetStoreSingleProductViewModel(id);
            return Product == null ? NotFound() : View(Product);
        }
    }
}

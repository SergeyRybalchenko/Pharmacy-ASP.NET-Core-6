using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy.Models;
using Pharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Service;
using Pharmacy.Domain;

namespace Pharmacy.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StoreController(ApplicationDBContext context)
        {
            _context = context;
        }       

        public async Task<IActionResult> Index(string SearchString, string SortType, int PageNumber = 1)
        { 
            var ProductService = new ProductServices(_context);
            var PagerService = new PagerServices();

            var Products = ProductService.GetStoreProductViewModel(SortType, SearchString);
            var Pager = PagerService.GetPagerViewModel(PageNumber, Products);
            Products = PagerService.SkipProducts(Pager, Products, PageNumber);

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
            var ProductService = new ProductServices(_context);
            var Product = ProductService.GetStoreSingleProductViewModel(id);
            return Product == null ? NotFound() : View(Product);
        }
    }
}

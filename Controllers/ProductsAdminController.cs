using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain;
using Pharmacy.Domain.Entities;
using Pharmacy.Models;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Controllers
{
    public class ProductsAdminController : Controller
    {
        private readonly IPagerService _pagerService;
        private readonly IProductService _productService;
        private const int pageSize = 9;

        public ProductsAdminController(IPagerService pagerService, IProductService productService)
        {
            _pagerService = pagerService;
            _productService = productService;
        }

        // GET: ProductsAdmin
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int PageNumber = 1)
        {

            var Products = _productService.GetAdminProductViewModels();
            var Pager = _pagerService.GetPagerViewModel(PageNumber, Products);
            Products = _pagerService.SkipProducts(Pager, Products, PageNumber);

            ViewBag.Pager = Pager;

            return View(Products);
        }


        // GET: ProductsAdmin/Details/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Details(Guid id)
        {
            var product = _productService.GetAdminProductViewModel(id);
            return product == null ? NotFound() : View(product);
        }

        // GET: ProductsAdmin/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create() => View();


        // POST: ProductsAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([Bind("ProductId,Name,Description,Price,ImagePath,Count,CreatedAt")] AdminProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = Guid.NewGuid();
                product.CreatedAt = DateTime.Now;
                _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetAdminProductViewModel(id);

            return product == null ? NotFound() : View(product);
        }

        // POST: ProductsAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid id, [Bind("ProductId,Name,Description,Price,ImagePath,Count,CreatedAt")] AdminProductViewModel product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productService.EditProduct(product);        
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(Guid id)
        {
            var product = _productService.GetAdminProductViewModel(id);
            return product == null ? NotFound() : View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var product = _productService.GetAdminProductViewModel(id);

            if (product != null)
            {
                _productService.DeleteProduct(product);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}

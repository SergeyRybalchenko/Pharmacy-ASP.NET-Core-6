using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Controllers
{
    public class ProductsAdminController : Controller
    {
        private readonly IPagerService _pagerService;
        private readonly IProductService _productService;

        public ProductsAdminController(IPagerService pagerService, IProductService productService)
        {
            _pagerService = pagerService;
            _productService = productService;
        }

        // GET: ProductsAdmin
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(int PageNumber = 1)
        {
            var Products = await _productService.GetAdminProductViewModels();

            var Pager = _pagerService.GetPagerViewModel(PageNumber, Products);

            Products = _pagerService.SkipProducts(Pager, Products, PageNumber);

            ViewBag.Pager = Pager;

            return View(Products);
        }


        // GET: ProductsAdmin/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetAdminProductViewModel(id);

            return product == null ? NotFound() : View(product);
        }

        // GET: ProductsAdmin/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create() => View();


        // POST: ProductsAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Price,ImagePath,Count,CreatedAt")] AdminProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = Guid.NewGuid();
                product.CreatedAt = DateTime.Now;
                await _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetAdminProductViewModel(id);
            return product == null ? NotFound() : View(product);
        }

        // POST: ProductsAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,Name,Description,Price,ImagePath,Count,CreatedAt")] AdminProductViewModel product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.EditProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetAdminProductViewModel(id);
            return product == null ? NotFound() : View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productService.GetAdminProductViewModel(id);

            if (product != null)
            {
                await _productService.DeleteProduct(product);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

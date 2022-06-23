using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy.Models;
using Pharmacy.Data;
using Pharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StoreController(ApplicationDBContext context) => _context = context;       

        public async Task<IActionResult> Index(string SearchString, string SortType, int pg = 1)
        {
            var products = await _context.Products.ToListAsync();

            const int pageSize = 9;

            if (pg < 1) pg = 1;
            int recsCount = products.Count();

            var pager = new PagerViewModel(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            products = products.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            List<StoreProductViewModel> model = new List<StoreProductViewModel> { };

            ViewData["CurrentFilter"] = SearchString;

            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.ToLower().Contains(SearchString.ToLower())).ToList();
            }

            if (SortType != null)
                switch (SortType)
                {
                    case "Name, A to Z":
                        products = products.OrderBy(p => p.Name).ToList();
                        break;
                    case "Name, Z to A":
                        products = products.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "Price, low to high":
                        products = products.OrderBy(p => p.Price).ToList();
                        break;
                    case "Price, high to low":
                        products = products.OrderByDescending(p => p.Price).ToList();
                        break;
                }

            for (var i = 0; i < products.Count; i++)
            {
                model.Add(new StoreProductViewModel
                {
                    Id = products[i].Id,
                    Name = products[i].Name,
                    Price = products[i].Price,
                    ImagePath = products[i].ImagePath
                });
            }


            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            
            if(product == null) return NotFound();

            StoreSingleProductViewModel model = new StoreSingleProductViewModel 
            { 
                Id = product.Id,
                Name = product.Name, 
                Description = product.Description, 
                Count = product.Count, 
                Price = product.Price,
                ImagePath = product.ImagePath
            };
            return View(model);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq;
using Pharmacy.Domain.Repositories.Abstract;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Domain.Repositories.EntityFramework
{
    public class EFProduct : IProducts
    {
        private readonly ApplicationDBContext _context;

        public EFProduct(ApplicationDBContext context)
            => _context = context;
      

        public async Task<List<Product>> GetProducts(string? SearchString = null)
        {

            var result = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))      
                result = result.Where(p => p.Name.ToLower().Contains(SearchString.ToLower()));     
           
            return await result.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            var test = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == Id);
            return test;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {       
            _context.Products.Remove(new Product() { ProductId = product.ProductId });
            await _context.SaveChangesAsync();
        }

    }
}

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
        {
            _context = context;
        }

        public List<Product> GetProducts(string? SearchString = null)
        {

            var result = new List<Product> { };

            if (!string.IsNullOrEmpty(SearchString))
            {
                result = _context.Products.Where(p => p.Name.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            else
            {
                result = _context.Products.ToList();
            }

            return result;
        }

        public Product GetProductById(Guid Id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(x => x.ProductId == Id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(new Product() { ProductId = product.ProductId });
            _context.SaveChanges();
        }

    }
}

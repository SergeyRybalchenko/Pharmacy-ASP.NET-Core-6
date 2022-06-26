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

        public List<Product> GetProducts(string SearchString)
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
            return _context.Products.FirstOrDefault(x => x.ProductId == Id);
        }

        public void SaveProduct(Product product)
        {
            _context.Entry(product).State = product.ProductId == default ? EntityState.Added : EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(Guid Id)
        {
            _context.Products.Remove(new Product() { ProductId = Id });
            _context.SaveChanges();
        }

    }
}

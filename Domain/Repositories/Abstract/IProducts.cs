using Pharmacy.Domain.Entities;

namespace Pharmacy.Domain.Repositories.Abstract
{
    public interface IProducts
    {
        List<Product> GetProducts(string SearchString);
        public Product GetProductById(Guid Id);
        public void SaveProduct(Product product);
        public void DeleteProduct(Guid Id);
    }
}
    
using Pharmacy.Domain.Entities;

namespace Pharmacy.Domain.Repositories.Abstract
{
    public interface IProducts
    {
        List<Product> GetProducts(string SearchString);
        public Product GetProductById(Guid Id);
        public void AddProduct(Product product);
        public void EditProduct(Product product);
        public void DeleteProduct(Product product);
    }
}
    
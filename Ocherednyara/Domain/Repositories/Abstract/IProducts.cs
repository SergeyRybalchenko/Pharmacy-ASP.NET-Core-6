using Pharmacy.Domain.Entities;

namespace Pharmacy.Domain.Repositories.Abstract
{
    public interface IProducts
    {
        public Task<List<Product>> GetProducts(string SearchString);
        public Task<Product> GetProductById(Guid Id);
        public Task AddProduct(Product product);
        public Task EditProduct(Product product);
        public Task DeleteProduct(Product product);
    }
}

using Pharmacy.Models;

namespace Pharmacy.Service.Abstract
{
    public interface IProductService
    {
        public StoreSingleProductViewModel GetStoreSingleProductViewModel(Guid Id);
        public List<StoreProductViewModel> GetStoreProductViewModel(string SortType, string SearchString);
        public List<AdminProductViewModel> GetAdminProductViewModels();
        public AdminProductViewModel GetAdminProductViewModel(Guid Id);
        public void AddProduct(AdminProductViewModel product);
        public void EditProduct(AdminProductViewModel product);
        public void DeleteProduct(AdminProductViewModel product);
    }
}

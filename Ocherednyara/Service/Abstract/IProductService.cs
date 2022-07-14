using Pharmacy.Models;

namespace Pharmacy.Service.Abstract
{
    public interface IProductService
    {
        public Task<StoreSingleProductViewModel> GetStoreSingleProductViewModel(Guid Id);
        public Task<List<StoreProductViewModel>> GetStoreProductViewModel(string SortType, string SearchString);
        public Task<List<AdminProductViewModel>> GetAdminProductViewModels();
        public Task<AdminProductViewModel> GetAdminProductViewModel(Guid Id);
        public Task AddProduct(AdminProductViewModel product);
        public Task EditProduct(AdminProductViewModel product);
        public Task DeleteProduct(AdminProductViewModel product);
        public Task<List<StoreProductViewModel>> GetHomeStoreProductViewModelList();
    }
}

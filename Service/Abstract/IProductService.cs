using Pharmacy.Models;

namespace Pharmacy.Service.Abstract
{
    public interface IProductService
    {
        public StoreSingleProductViewModel GetStoreSingleProductViewModel(Guid Id);
        public List<StoreProductViewModel> GetStoreProductViewModel(string SortType, string SearchString);
    }
}

using Pharmacy.Models;

namespace Pharmacy.Service.Abstract
{
    public interface IPagerService
    {
        public PagerViewModel GetPagerViewModel(int PageNumber, List<StoreProductViewModel> Products);
        public List<StoreProductViewModel> SkipProducts(PagerViewModel Pager, List<StoreProductViewModel> Products, int PageNumber);
    }
}

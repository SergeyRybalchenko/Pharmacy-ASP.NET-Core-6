using Pharmacy.Models;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Service
{
    public class PagerService : IPagerService
    {
        private const int PageSize = 9;

        public PagerViewModel GetPagerViewModel(int PageNumber, List<StoreProductViewModel> Products)
        {
            
            if (PageNumber < 1) PageNumber = 1;

            int RowsCount = Products.Count();

            var Pager = new PagerViewModel(RowsCount, PageNumber, PageSize);

            return Pager;
        }

        public List<StoreProductViewModel> SkipProducts(PagerViewModel Pager, List<StoreProductViewModel> Products, int PageNumber)
        {
            int TotalRowsSkip = (PageNumber - 1) * PageSize;

            return Products.Skip(TotalRowsSkip).Take(Pager.PageSize).ToList();
        }
    }
}

using Pharmacy.Models;

namespace Pharmacy.Service
{
    public class PagerServices
    {
        private const int PageSize = 9;

        public PagerViewModel GetPagerViewModel(int PageNumber, List<StoreProductViewModel> Products)
        {
            
            if (PageNumber < 1) PageNumber = 1;
            int RowsCount = Products.Count();
            var Pager = new PagerViewModel(RowsCount, PageNumber, PageSize);

            //int TotalRowsSkip = (PageNumber - 1) * PageSize;

            //products = products.Skip(TotalRowsSkip).Take(Pager.PageSize).ToList();

            return Pager;
        }

        public List<StoreProductViewModel> SkipProducts(PagerViewModel Pager, List<StoreProductViewModel> Products, int PageNumber)
        {
            int TotalRowsSkip = (PageNumber - 1) * PageSize;
            return Products.Skip(TotalRowsSkip).Take(Pager.PageSize).ToList();
        }
    }
}

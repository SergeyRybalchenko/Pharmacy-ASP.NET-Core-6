using Pharmacy.Models;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Service
{
    public class PagerService : IPagerService
    {
        private const int PageSize = 9;

        /// <summary>
        /// Return PagerViewModel to use it in View and skip some products
        /// </summary>
        /// <param name="PageNumber">Current page</param>
        /// <param name="Products">List of T</param>
        /// <returns>PagerViewModel</returns>
        public PagerViewModel GetPagerViewModel<T>(int PageNumber, List<T> Products)
        {

            if (PageNumber < 1) PageNumber = 1;

            int RowsCount = Products.Count();

            var Pager = new PagerViewModel(RowsCount, PageNumber, PageSize);

            return Pager;
        }

        /// <summary>
        /// Skips a certain number of products to the сurrent page
        /// </summary>
        /// <param name="Pager">PagerViewModel created by GetPagerViewModel</param>
        /// <param name="Products">List of T to skip some products</param>
        /// <param name="PageNumber">Current page</param>
        /// <returns>List of T for PageNumber page</returns>
        public List<T> SkipProducts<T>(PagerViewModel Pager, List<T> Products, int PageNumber)
        {
            int TotalRowsSkip = (PageNumber - 1) * PageSize;

            return Products.Skip(TotalRowsSkip).Take(Pager.PageSize).ToList();
        }
    }
}

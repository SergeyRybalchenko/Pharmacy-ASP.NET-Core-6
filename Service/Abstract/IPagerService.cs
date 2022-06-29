using Pharmacy.Models;

namespace Pharmacy.Service.Abstract
{
    public interface IPagerService
    {
        public PagerViewModel GetPagerViewModel<T>(int PageNumber, List<T> Products);
        public List<T> SkipProducts<T>(PagerViewModel Pager, List<T> Products, int PageNumber);
    }
}

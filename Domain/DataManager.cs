using Pharmacy.Domain.Repositories.Abstract;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Domain
{
    public class DataManager
    {
        public IProducts Products { get; set; }

        public DataManager(IProducts products)
        {
            Products = products;
        }
    }
}

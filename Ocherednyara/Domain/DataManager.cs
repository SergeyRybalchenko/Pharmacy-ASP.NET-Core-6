using Pharmacy.Domain.Repositories.Abstract;

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

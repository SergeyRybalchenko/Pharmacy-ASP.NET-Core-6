using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Repositories.EntityFramework;
using Pharmacy.Data;
using Pharmacy.Models;
using Pharmacy.Domain;
using Pharmacy.Service.Abstract;

namespace Pharmacy.Service
{
    public class ProductService : IProductService
    {
        private readonly DataManager _dataManger;

        public ProductService(DataManager dataManager)
        {
            _dataManger = dataManager;
        }

        public List<StoreProductViewModel> GetStoreProductViewModel(string SortType, string SearchString)
        {
            var result = _dataManger.Products.GetProducts(SearchString)
              .Select(x => new StoreProductViewModel
              {
                  Id = x.ProductId,
                  Name = x.Name,
                  Price = x.Price,
                  ImagePath = x.ImagePath
              }).ToList();

            switch (SortType)
            {
                case "Name, A to Z":
                    result = result.OrderBy(p => p.Name).ToList();
                    break;
                case "Name, Z to A":
                    result = result.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Price, low to high":
                    result = result.OrderBy(p => p.Price).ToList();
                    break;
                case "Price, high to low":
                    result = result.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    break;
            }

            return result;
        }

        public StoreSingleProductViewModel GetStoreSingleProductViewModel(Guid Id)
        {
            var test = _dataManger.Products.GetProductById(Id);

            var result = new StoreSingleProductViewModel 
            {
                Id = test.ProductId,
                Name = test.Name,
                Description = test.Description,
                Count = test.Count,
                Price = test.Price,
                ImagePath = test.ImagePath
            };

            return result;
        }
    }
}

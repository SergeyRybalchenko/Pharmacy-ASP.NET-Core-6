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
        private readonly DataManager _dataManager;

        public ProductService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        /// <summary>
        /// Returns list of StoreProductViewModel to use it in Store Index View
        /// </summary>
        /// <param name="SortType">Sort type</param>
        /// <param name="SearchString">Product search string</param>
        /// <returns>List of StoreProductViewModels</returns>
        public List<StoreProductViewModel> GetStoreProductViewModel(string SortType, string SearchString)
        {
            var result = _dataManager.Products.GetProducts(SearchString)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AdminProductViewModel> GetAdminProductViewModels()       
            => _dataManager.Products.GetProducts("").Select(x => new AdminProductViewModel
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.ImagePath,
                Count = x.Count,
                CreatedAt = x.CreatedAt
            }).ToList();
        

        /// <summary>
        /// Return more info about product to show it in details
        /// </summary>
        /// <param name="Id">Product identifier</param>
        /// <returns>StoreSingleProductViewModel</returns>
        public StoreSingleProductViewModel GetStoreSingleProductViewModel(Guid Id)
        {
            var test = _dataManager.Products.GetProductById(Id);

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

        public AdminProductViewModel GetAdminProductViewModel(Guid Id)
        {
            var test = _dataManager.Products.GetProductById(Id);

            var result = new AdminProductViewModel
            {
                ProductId = test.ProductId,
                Name = test.Name,
                Description = test.Description,
                Price = test.Price,
                ImagePath = test.ImagePath,
                Count = test.Count,
                CreatedAt = test.CreatedAt
            };

            return result;
        }

        public void AddProduct(AdminProductViewModel product)
        {
            var newProduct = new Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                Count = product.Count,
                CreatedAt = product.CreatedAt
            };

            _dataManager.Products.AddProduct(newProduct);
        }

        public void DeleteProduct(AdminProductViewModel product)
        {
            var delProduct = new Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                Count = product.Count,
                CreatedAt = product.CreatedAt
            };

            _dataManager.Products.DeleteProduct(delProduct);
        }

        public void EditProduct(AdminProductViewModel product)
        {
            var editProduct = new Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                Count = product.Count,
                CreatedAt = product.CreatedAt
            };

            _dataManager.Products.EditProduct(editProduct);
        }

    }
}

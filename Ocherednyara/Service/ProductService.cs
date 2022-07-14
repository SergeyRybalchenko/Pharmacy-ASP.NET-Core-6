using Pharmacy.Domain;
using Pharmacy.Domain.Entities;
using Pharmacy.Models;
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
        public async Task<List<StoreProductViewModel>> GetStoreProductViewModel(string SortType, string SearchString)
        {
            var products = await _dataManager.Products.GetProducts(SearchString);

            var Result = products.Select(x => new StoreProductViewModel
            {
                Id = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                ImagePath = x.ImagePath
            }).ToList();

            switch (SortType)
            {
                case "Name, A to Z":
                    Result = Result.OrderBy(p => p.Name).ToList();
                    break;
                case "Name, Z to A":
                    Result = Result.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Price, low to high":
                    Result = Result.OrderBy(p => p.Price).ToList();
                    break;
                case "Price, high to low":
                    Result = Result.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    break;
            }

            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdminProductViewModel>> GetAdminProductViewModels()
        {
            var result = await _dataManager.Products.GetProducts("");

            return result.Select(x => new AdminProductViewModel
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.ImagePath,
                Count = x.Count,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        /// <summary>
        /// Return more info about product to show it in details
        /// </summary>
        /// <param name="Id">Product identifier</param>
        /// <returns>StoreSingleProductViewModel</returns>
        public async Task<StoreSingleProductViewModel> GetStoreSingleProductViewModel(Guid Id)
        {
            var test = await _dataManager.Products.GetProductById(Id);

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

        public async Task<AdminProductViewModel> GetAdminProductViewModel(Guid Id)
        {
            var test = await _dataManager.Products.GetProductById(Id);

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

        public async Task AddProduct(AdminProductViewModel product)
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

            await _dataManager.Products.AddProduct(newProduct);
        }

        public async Task DeleteProduct(AdminProductViewModel product)
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

            await _dataManager.Products.DeleteProduct(delProduct);
        }

        public async Task EditProduct(AdminProductViewModel product)
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

            await _dataManager.Products.EditProduct(editProduct);
        }

        public async Task<List<StoreProductViewModel>> GetHomeStoreProductViewModelList()
        {
            var Products = await _dataManager.Products.GetProducts("");
            var BestProducts = await _dataManager.Products.GetProducts("");

            Products = Products.OrderByDescending(x => x.CreatedAt).Take(4).ToList();
            BestProducts = BestProducts.OrderByDescending(x => x.Price).Take(6).ToList();

            foreach (var product in BestProducts) Products.Add(product);

            var storeProductViewModels = Products.Select(x => new StoreProductViewModel
            {
                Id = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                ImagePath = x.ImagePath,
            }).ToList();

            return storeProductViewModels;
        }

    }
}

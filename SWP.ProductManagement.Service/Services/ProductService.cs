using SWP.ProductManagement.Repository;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.ProductManagement.Service.Services
{
    public class ProductService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(int page, int pageSize)
        {
            var products = await _unitOfWork.Products.GetAllAsync(null, page, pageSize, "Category");
            return products.Select(product => new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = (int)product.UnitsInStock,
                UnitPrice = (decimal)product.UnitPrice,
                CategoryName = product.Category.CategoryName
            });
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync("ProductId",id, "Category");
            if(product == null)
            {
                return null;
            }
            return new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = (int)product.UnitsInStock,
                UnitPrice = (decimal)product.UnitPrice,
                CategoryName = product.Category.CategoryName
            };
        }
        public async Task<bool> UpdateProductAsync(int id, ProductModel productModel)
        {
            var productToUpdate = await _unitOfWork.Products.GetByIdAsync("ProductId", id);
            var categoryToUpdate = await _unitOfWork.Categories.GetByIdAsync("CategoryId", id);
            if( productToUpdate == null )
            {
                return false;
            }

            productToUpdate.ProductName = productModel.ProductName;
            productToUpdate.UnitsInStock = productModel.UnitsInStock;
            productToUpdate.UnitPrice = productModel.UnitPrice;
            productToUpdate.CategoryId = productModel.CategoryId;
            productToUpdate.Category = categoryToUpdate;

            _unitOfWork.Products.Update(productToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<int> InsertProductAsync(ProductModel productModel)
        {
            var productEntity = new Product
            {
                ProductName = productModel.ProductName,
                UnitsInStock = productModel.UnitsInStock,
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId,
            };

            await _unitOfWork.Products.CreateAsync(productEntity);
            await _unitOfWork.SaveAsync();
            return productEntity.ProductId;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync("ProductId", id);
            if (product == null) { return false; }
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveAsync();
            return true;
        }
        public async Task<bool> ProductExistAsync(int id)
        {
            return await _unitOfWork.Products.IsExist(id);
        }

        public async Task<IEnumerable<ProductModel>> SearchProductAsync(string? productName, decimal? unitPrice, int? unitsInStock, int page, int pageSize)
        {
            var products = await _unitOfWork.Products.SearchAsync(productName, unitPrice, unitsInStock, page, pageSize);
            return products.Select(product => new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = (int)product.UnitsInStock,
                UnitPrice = (decimal)product.UnitPrice,
                CategoryName = product.Category.CategoryName
            });
        }
    }

}

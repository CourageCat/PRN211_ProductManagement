using SWP.ProductManagement.Repository;
using SWP.ProductManagement.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP.ProductManagement.Repository.Models;

namespace SWP.ProductManagement.Service.Services
{
    public class CategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync(int page, int pageSize)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, page, pageSize, "Products");
            return categories.Select(category => new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products.Select(product => new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitsInStock = (int)product.UnitsInStock,
                    UnitPrice = (decimal)product.UnitPrice,
                    CategoryId = category.CategoryId,
                }).ToList()
            });
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync("CategoryId", id, "Products");
            if(category == null)
            {
                return null;
            }
            return new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products.Select(product => new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                }).ToList(),
            };
        }

        public async Task<int> InsertCategoryAsync(CategoryModel category)
        {
            var categoryEntity = new Category
            {
                CategoryName = category.CategoryName
            };
            await _unitOfWork.Categories.CreateAsync(categoryEntity);
            await _unitOfWork.SaveAsync();
            return categoryEntity.CategoryId;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryModel categoryModel)
        {
            var categoryToUpdate = await _unitOfWork.Categories.GetByIdAsync("CategoryId", id);
            if (categoryToUpdate == null) return false;
            categoryToUpdate.CategoryName = categoryModel.CategoryName;
            _unitOfWork.Categories.UpdateAsync(categoryToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync("CategoryId", id);
            if(category == null) return false;
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> CategoryExistAsync(int id)
        {
            return await _unitOfWork.Categories.IsExist(id);
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategoryIdAsync(int id)
        {
            var products = await _unitOfWork.Products.GetAllAsync(p => p.CategoryId == id);
            return products.Select(product => new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = (int)product.UnitsInStock,
                UnitPrice = (decimal)product.UnitPrice,
                CategoryId = (int)product.CategoryId
            });
        }

    }
}

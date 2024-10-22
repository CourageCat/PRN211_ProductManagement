using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.ProductManagement.API.RequestModels;
using SWP.ProductManagement.API.ResponseModels;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Service.BusinessModels;
using SWP.ProductManagement.Service.Services;

namespace SWP.ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryResponseModel>>> GetCategories([FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            var categories = await _categoryService.GetCategoriesAsync(page, pageSize);
            var response = categories.Select(category => new CategoryResponseModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = (List<ProductResponseModel>)category.Products.Select(product => new ProductResponseModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitsInStock = product.UnitsInStock,
                    UnitPrice = product.UnitPrice,
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<CategoryResponseModel>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var response = new CategoryResponseModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = (List<ProductResponseModel>)category.Products.Select(product => new ProductResponseModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitsInStock = product.UnitsInStock,
                    UnitPrice = product.UnitPrice,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost("category")]
        public async Task<ActionResult> CreateCategory(CategoryRequestModel request)
        {
            var categoryModel = new CategoryModel
            {
                CategoryName = request.CategoryName,
            };
            var result = await _categoryService.InsertCategoryAsync(categoryModel);
            categoryModel.CategoryId = result;
            return CreatedAtAction(nameof(GetCategoryById), new
            {
                id = categoryModel.CategoryId,
            }, categoryModel);
        }

        [HttpPut("category/{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryRequestModel request)
        {
            var categoryModel = new CategoryModel
            {
                CategoryName = request.CategoryName,
            };
            var success = await _categoryService.UpdateCategoryAsync(id, categoryModel);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("category/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}

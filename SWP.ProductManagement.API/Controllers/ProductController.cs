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
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductResponseModel>>> GetProducts([FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync(page, pageSize);
            var response = products.Select(product => new ProductResponseModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                CategoryName = product.CategoryName,
            });

            return Ok(response);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            var response = new ProductResponseModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                CategoryName = product.CategoryName,
            };

            return Ok(response);
        }

        [HttpPost("product")]
        public async Task<ActionResult> CreateProduct(ProductRequestModel request)
        {
            var productModel = new ProductModel
            {
                ProductName = request.ProductName,
                UnitsInStock = request.UnitsInStock,
                UnitPrice = request.UnitPrice,
                CategoryId = request.CategoryId,
            };
            var result = await _productService.InsertProductAsync(productModel);
            productModel.ProductId = result;
            return CreatedAtAction(nameof(GetProductById), new
            {
                id = productModel.ProductId,
            }, productModel);
        }

        [HttpPut("product/{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductRequestModel request)
        {
            var productModel = new ProductModel
            {
                ProductName = request.ProductName,
                UnitsInStock = request.UnitsInStock,
                UnitPrice = request.UnitPrice,
                CategoryId = request.CategoryId,
            };
            var success = await _productService.UpdateProductAsync(id, productModel);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("product/search")]
        public async Task<ActionResult<ProductResponseModel>> SearchProduct([FromQuery] ProductSearchRequestModel productSearch, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            string? productName = productSearch.ProductName;
            decimal? unitPrice = productSearch.UnitPrice;
            int? unitsInStock = productSearch.UnitsInStock;
            var products = await _productService.SearchProductAsync(productName, unitPrice, unitsInStock, page, pageSize);
            if (products == null)
            {
                return NotFound();
            }
            var response = products.Select(product => new ProductResponseModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                CategoryName = product.CategoryName,
            });
            return Ok(response);

        }

    }
}

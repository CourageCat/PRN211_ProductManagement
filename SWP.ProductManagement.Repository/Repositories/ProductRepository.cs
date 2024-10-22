using Microsoft.EntityFrameworkCore;
using SWP.ProductManagement.Repository.Base;
using SWP.ProductManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.ProductManagement.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository()
        {
        }

        public ProductRepository(ProductManagementContext context) => _context = context;

        public async Task<bool> IsExist(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            return result == null;
        }

        public async Task<List<Product>> SearchAsync(string? productName, decimal? unitPrice, int? unitsInStock, int page = 1, int pageSize = 10)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Apply filters based on the request (AND logic)
            if (productName != null)
            {
                query = query.Where(p => p.ProductName.Contains(productName));
            }
            if (unitPrice != null)
            {
                query = query.Where(p => p.UnitPrice == unitPrice);
            }
            if (unitsInStock != null)
            {
                query = query.Where(p => p.UnitsInStock == unitsInStock);
            }
            // Apply pagination with Skip and Take
            var totalItems = await query.CountAsync();
            var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return products;

        }
    }
}

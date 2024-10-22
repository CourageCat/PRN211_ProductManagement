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
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository()
        {
        }

        public CategoryRepository(ProductManagementContext context) => _context = context;

        public async Task<bool> IsExist(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
            return result == null;
        }
    }
}

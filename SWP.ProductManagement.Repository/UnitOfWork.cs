using SWP.ProductManagement.Repository.Base;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Repository.Repositories;

namespace SWP.ProductManagement.Repository
{
    public class UnitOfWork : IDisposable
    {
        private ProductManagementContext _context;
        private CategoryRepository _categoryRepository;
        private ProductRepository _productRepository;
        private bool disposed = false;

        public UnitOfWork(ProductManagementContext context) => _context = context;

        public CategoryRepository Categories
        {
            get
            {
                if(this._categoryRepository == null)
                {
                    this._categoryRepository = new CategoryRepository();
                }
                return this._categoryRepository;
            }
        }
        public ProductRepository Products
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new ProductRepository();
                }
                return this._productRepository;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP.ProductManagement.Repository.Models;
using System.Linq.Expressions;

namespace SWP.ProductManagement.Repository.Base
{
    public class GenericRepository<T> where T : class
    {
        protected ProductManagementContext _context;

        public GenericRepository() => _context ??= new ProductManagementContext();

        public GenericRepository(ProductManagementContext context) => _context = context;
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
            //return _context.Set<T>().AsNoTracking().ToList();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        #region Asynchronous


        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, int page = 1, int pageSize = 10, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Include the related entities specified
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Apply the filter condition if provided
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<T> GetByIdAsync(string idOfEntity, int id, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Include the related entities specified
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, $"{idOfEntity}") == id);
        }

        public async Task<T> GetByIdAsync(string code, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Code") == code);
        }

        public async Task<T> GetByIdAsync(Guid code, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Code") == code);
        }

        #endregion


        #region Separating asigned entities and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}


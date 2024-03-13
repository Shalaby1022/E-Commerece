using Core.Interfaces;
using Core.Specifications;
using InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(StoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
             await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
             _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
          IEnumerable<string> Includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (Includes != null)
            {
                foreach (var IncludeProperty in Includes)
                {
                    query = query.Include(IncludeProperty);
                }
            }

            if (OrderBy != null)
            {
                query = OrderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }


             return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }
        

        public async Task UpdateAsync(T entity)
        {
             _dbSet.Update(entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetWithSpecificationAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvualtor<T>.GetQuery(_dbSet.AsQueryable(),specification);
        }
    }
}
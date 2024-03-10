using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync( Expression<Func<T , bool>> expression
                           , Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null
                           , IEnumerable<string> Includes = null);

        Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetWithSpecificationAsync(ISpecification<T> specification);

        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);


    }
}

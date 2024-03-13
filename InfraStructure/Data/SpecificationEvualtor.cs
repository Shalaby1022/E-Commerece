using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Data
{
    public class SpecificationEvualtor<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputquery , ISpecification<T> specification) 
        {
            var query = inputquery;

            if(specification.Expression!= null)
            {
                query = query.Where(specification.Expression);
            }

            if (specification.OrderByAscending != null)
            {
                query = query.OrderBy(specification.OrderByAscending);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }


            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;

        }
    }
}

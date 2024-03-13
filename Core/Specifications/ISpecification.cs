using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Expression { get; }

        List<Expression<Func<T,object>>> Includes { get;}

        Expression<Func<T , object>> OrderByAscending { get;}

        Expression<Func<T, object>> OrderByDescending { get; }



    }
}

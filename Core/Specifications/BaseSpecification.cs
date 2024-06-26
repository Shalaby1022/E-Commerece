﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {
                
        }
        public BaseSpecification( Expression<Func<T,bool>> expression)

        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public Expression<Func<T, bool>> Expression { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderByAscending {get; private set;}

        public Expression<Func<T, object>> OrderByDescending { get; private set;}

        public int Skip {  get; private set;}

        public int Take { get; private set;}

        public bool IsPaging { get; private set;}

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderByAsc(Expression<Func<T, object>> orderByExpression)
        {
            OrderByAscending = orderByExpression;
                
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPagination(int skip , int take)
        {
            Skip = skip;
            Take = take;
            IsPaging = true;
        }

    }
}

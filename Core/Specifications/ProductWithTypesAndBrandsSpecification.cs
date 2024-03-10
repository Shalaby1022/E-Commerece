using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandxsSpecification : BaseSpecification<Product>
    {

        public ProductWithTypesAndBrandxsSpecification(int id ) : base(x=>x.Id == id)
        {
            AddInclude(x => x.productType);
            AddInclude(y => y.ProductBrand);
        }
        public ProductWithTypesAndBrandxsSpecification()
        {
                AddInclude(x => x.productType);
                AddInclude(y => y.ProductBrand);
        }
    }
}

using Core.Models;
using E_Commerece.API.ResourcceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithFilterWithCountSpecififcation : BaseSpecification<Product>
    {
        public ProductsWithFilterWithCountSpecififcation(ProductResourceParameters productResourceParameters) : base(x =>

        (!productResourceParameters.TypeId.HasValue || x.ProductTypeId == productResourceParameters.TypeId) &&
        (!productResourceParameters.BrandId.HasValue || x.ProductBrandId == productResourceParameters.BrandId))

        {

            
        }
        
    }
}

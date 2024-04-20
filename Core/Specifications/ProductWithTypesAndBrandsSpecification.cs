using Core.Models;
using E_Commerece.API.ResourcceParameters;
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
        public ProductWithTypesAndBrandxsSpecification(ProductResourceParameters productResourceParameters)
            : base(x =>
                (string.IsNullOrEmpty(productResourceParameters.Seacrh) || x.Name.ToLower().Contains(productResourceParameters.Seacrh)) &&
                (!productResourceParameters.TypeId.HasValue || x.ProductTypeId == productResourceParameters.TypeId) &&
                (!productResourceParameters.BrandId.HasValue || x.ProductBrandId == productResourceParameters.BrandId))
        {

                 AddInclude(x => x.productType);
                 AddInclude(y => y.ProductBrand);
                 AddOrderByAsc(x => x.Name);
                 ApplyPagination(productResourceParameters.PageSize * (productResourceParameters.PageIndex - 1) , productResourceParameters.PageSize);



            if(!string.IsNullOrEmpty(productResourceParameters.Sort))
            {
                switch (productResourceParameters.Sort)
                {
                    case "PriceAsc":
                        AddOrderByAsc(p => p.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    case "NameAsc":
                        AddOrderByAsc(p => p.Name);
                        break;

                    case "NameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;

                }
            }
        }
    }
}

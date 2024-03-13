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
        public ProductWithTypesAndBrandxsSpecification(string sort, int? typeId, int? brandId)
            : base(x =>
                (!typeId.HasValue || x.ProductTypeId == typeId) &&
                (!brandId.HasValue || x.ProductBrandId == brandId))
        {

                 AddInclude(x => x.productType);
                 AddInclude(y => y.ProductBrand);
                 AddOrderByAsc(x => x.Name);


            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        AddOrderByAsc(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    case "Name":
                        AddOrderByAsc(n => n.Name);
                        break;
                }
            }
        }
    }
}

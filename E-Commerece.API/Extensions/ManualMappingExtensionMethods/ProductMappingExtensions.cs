using Core.Models;
using E_Commerece.API.DTOs.Product;

namespace E_Commerece.API.Extensions.ManualMappingExtensionMethods
{
    public static class ProductMappingExtensions
    {
        public static ProductDto ToProductDtoFromProduct(this Product product)
        {
           return new ProductDto
            {
                Name = product.Name,
                Price = product.Price,  
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                ProductType = product.productType,
                ProductBrand = product.ProductBrand,
            };

        }

        public static Product ToProductFromDtoInCreation(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.ProductName,
                Description = createProductDto.ProductDescription,
                Price = createProductDto.ProductionPrice,

            };
        }

    }
}

using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace InfraStructure.Data
{
    public class StoreContextSeed 
    {
        private readonly ModelBuilder _builder;

        public StoreContextSeed(ModelBuilder modelBuilder)
        {
            _builder = modelBuilder;
        }

    public static async Task SeedingData(StoreDbContext dbContext, ILogger<StoreContextSeed> logger)
        {
            try
            {
                if(!dbContext.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../InfraStructure/Data/DataSeeding/brands.json");

                    var selializerData = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


                    foreach (var item in selializerData)
                    {
                        dbContext.ProductBrands.Add(item);
                      
                    }
                    await dbContext.SaveChangesAsync();
                }

                if(!dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../InfraStructure/Data/DataSeeding/products.json");

                    var serializedData = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in serializedData)
                    {
                        dbContext.Products.Add(item);
                    }

                   await  dbContext.SaveChangesAsync();
                }

                if (!dbContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../InfraStructure/Data/DataSeeding/types.json");

                    var seliazlizedTypesData = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in seliazlizedTypesData)
                    {
                        dbContext.ProductTypes.Add(item);

                    }

                    await dbContext.SaveChangesAsync(true);

                }


            }
            catch (Exception ex)
            {
                logger.LogError("Internal Server Error Happend During seeding Data");
            }

        }
    }
}

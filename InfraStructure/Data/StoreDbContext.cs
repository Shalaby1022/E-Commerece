using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace InfraStructure.Data
{
    public class StoreDbContext : DbContext
    {
     public Microsoft.EntityFrameworkCore.DbSet<Product> Products { get; set; }
     public Microsoft.EntityFrameworkCore.DbSet<ProductBrand> ProductBrands { get; set; }
     public Microsoft.EntityFrameworkCore.DbSet<ProductType> ProductTypes { get; set; }



    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
                
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            async Task SeedData(ILogger<StoreContextSeed> logger)
            {
                await StoreContextSeed.SeedingData(this, logger);
            }



        }



    }
}

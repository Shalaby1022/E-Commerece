using Core.Interfaces;
using Core.Models;
using InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> CreateNewProductAsync(Product product)
        {
            var Product = await _context.Products.AddAsync(product);
            return await SaveAsync();
        }

        public async Task<Product> DeleteProudtcsAsync(Product product)
        {
            _context.Remove(product);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(s=>s.productType)
                .Include(s=>s.ProductBrand)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {

            if (id == null) throw new ArgumentNullException("No id matching this");

            var oneProduct = await _context.Products
                .Include(p=>p.productType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(a=>a.Id == id);

            if (oneProduct is null) throw new KeyNotFoundException("Can't Find This Specific Product For this Id! Try Another One");

            return oneProduct;
        }

        public async Task<Product> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return await SaveAsync();
            
        }

        public async Task<Product> UpdateExistingProductAsync(Product product)
        {
            _context.Update(product);
            return await SaveAsync();
        }
    }
}

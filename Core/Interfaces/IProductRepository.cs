using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateNewProductAsync(Product product);

        Task<Product> UpdateExistingProductAsync(Product product);
        Task<Product> DeleteProudtcsAsync(Product product);

        Task<Product> SaveAsync();

    }
}

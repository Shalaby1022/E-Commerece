using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepsoitory
    {
        Task<CustomerBasket> GetCustomerBasketAsync(string BasketId);

        Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket customerBasket);

        Task<bool> DeleteCustomerBasketAsync(string BasketId);




    }
}

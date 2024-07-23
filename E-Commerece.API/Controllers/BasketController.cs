using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.API.Controllers
{
    [ApiController]
    [Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepsoitory _basketRepsoitory;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepsoitory basketRepsoitory 
                               , ILogger<BasketController> logger)
        {
            _basketRepsoitory = basketRepsoitory ?? throw new ArgumentNullException(nameof(basketRepsoitory));
            _logger = logger;
        }

        [HttpGet("{basketId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerBasketById(string basketId)
        {
            try
            {
                var basket = await _basketRepsoitory.GetCustomerBasketAsync(basketId);
                

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(basket ?? new Core.Models.CustomerBasket(basketId));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing GettingBasket.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateExisitngCustomerBasket(CustomerBasket customerBasket)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(UpdateExisitngCustomerBasket)}");
                return BadRequest(ModelState);
            }

            try
            {
                var basketUpdating = await _basketRepsoitory.UpdateCustomerBasketAsync(customerBasket);
                return Ok(customerBasket);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Updating the Basket {nameof(UpdateExisitngCustomerBasket)}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{basketId:int}")]

        public async Task<ActionResult> DeleteExisitngCustomerBasket(string basketId)
        {
            try
            {
                var basketDeleted = await _basketRepsoitory.DeleteCustomerBasketAsync(basketId);
                if (basketId == null)
                { 
                    return NotFound("The Basket Youare trying to delete can't be found");
                }

                return Ok(basketDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Deleting the Basket {nameof(UpdateExisitngCustomerBasket)}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using E_Commerece.API.Extensions.ManualMappingExtensionMethods;
using Core.Models;
using System.ComponentModel.Design;
using E_Commerece.API.DTOs.Product;
using Core.Specifications;
using E_Commerece.API.ResourcceParameters;
using Microsoft.AspNetCore.Authorization;


namespace E_Commerece.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productGenericRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(   IGenericRepository<Product> productGenericRepository 
                                 ,  IProductRepository productRepository
                                 , ILogger<ProductController> logger
                                  )

        {
            _productGenericRepository = productGenericRepository;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        public async Task<IActionResult> GetProducts( [FromQuery] ProductResourceParameters resourceParameters)
        {
            try
            {

                var spec = new ProductWithTypesAndBrandxsSpecification(resourceParameters);

                var products = await _productGenericRepository.GetAllWithSpecificationAsync(spec);

                var mappedProducts = products.Select(c => c.ToProductDtoFromProduct());

                if (mappedProducts == null)
                {
                    ModelState.AddModelError("", "Mapping failed. Unable to create Comment.");
                    return BadRequest(ModelState);
                }

                return Ok(mappedProducts);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An Error Occurred While Retreiveing The Comments Info {nameof(GetProducts)}");
                return StatusCode(500, "Internal Server Error!!");


            }
        }
        [HttpGet("{PrdouctId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(int PrdouctId)
        {
            try
            {
                if (PrdouctId == null) return BadRequest("CommentId can't be NUll Value");

                var spec = new ProductWithTypesAndBrandxsSpecification(PrdouctId);

                var product = await _productGenericRepository.GetWithSpecificationAsync(spec);

                if (product == null)
                    return NotFound($"Product with Id {PrdouctId} not found");


                var mappedProduct = product.ToProductDtoFromProduct();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(mappedProduct);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing GetSpecificProduct.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddingNewProduct([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(AddingNewProduct)}");
                return BadRequest(ModelState);
            }

            try
            {
               if (createProductDto == null) return BadRequest(ModelState);

                var commentMap = createProductDto.ToProductFromDtoInCreation();

                if (commentMap == null)
                {
                    ModelState.AddModelError("", "Mapping failed. Unable to create Comment.");
                    return BadRequest(ModelState);
                }

               await _productRepository.CreateNewProductAsync(commentMap);

               return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating and adding new Comment {nameof(AddingNewProduct)}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

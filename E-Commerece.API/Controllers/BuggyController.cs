using E_Commerece.API.ExceptionsConfiguration.Exceptions;
using InfraStructure.Data;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerece.API.Controllers
{
    [ApiController]
    [Route("api/Buggy")]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {

            var anyThingAssumeProduct = _context.Products.Find(80);
            if(anyThingAssumeProduct == null)
            {
                throw new NotFoundException("Testing Not Found");   
            }

            return Ok();
        }

        [HttpGet("InternalServerErro")]
        public ActionResult GetInternalServerError()
        {
            var anyThingAssumeProduct = _context.Products.Find(20);
            if(anyThingAssumeProduct != null)
            {
                var anythingToString = anyThingAssumeProduct.ToString();
            }

            return (500 , new API.ExceptionsConfiguration.Errors.ApiExceptions(500));
        }

        [HttpGet("badRquest")]
        public ActionResult GetBadRquest()
        {
            return BadRequest(new API.ExceptionsConfiguration.Errors.ApiResponse(400));
        }

        [HttpGet("BadRquest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();

        }
        

    }
}

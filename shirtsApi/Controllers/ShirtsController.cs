using Microsoft.AspNetCore.Mvc;
using shirtsApi.Model;
using shirtsApi.Model.Filters.ActionFilters;
using shirtsApi.Model.Repository;
using shirtsApi.Model.Filters.ExceptionFilters;

namespace shirtsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {

        [HttpGet]
        public IActionResult getShirts()
        {
            return Ok(ShirtRepository.getAllShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult getShirtsById(int id)
        {
            return Ok(ShirtRepository.getShirtById(id));
        }

        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult createShirt([FromBody] Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(getShirtsById),
                new { id = shirt.ShirtId },
                shirt
                );
        }

        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter] //This metod overrides handleUpdate with no purpose
        [Shirt_ValidadeUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult updateShirt(int id, Shirt shirt)
        {
            ShirtRepository.updateShirt(shirt);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult deleteShirt(int id)
        {
            var shirt = ShirtRepository.getShirtById(id);
            ShirtRepository.deleteShirt(id);

            return Ok(shirt);
        }
    }
}

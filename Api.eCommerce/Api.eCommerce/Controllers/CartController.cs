using Api.eCommerce.EC;
using Library.eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.eCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return new CartEC().Get();
        }

        [HttpPost]
        public Item? AddOrUpdate([FromBody] Item item)
        {
            return new CartEC().AddOrUpdate(item);
        }

        [HttpDelete("{id}")]
        public Item? Delete(int id)
        {
            return new CartEC().Delete(id);
        }

        [HttpPost("clear")]
        public IActionResult ClearCart()
        {
            new CartEC().Clear();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Requests;
using ProductApp.Services;
using System.Net.Mime;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IOrderService _orderService;
        public ProductController(ILogger<ProductController> logger, 
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Route("createorder")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            var createOrderRes = await _orderService.CreateOrder(createOrderRequest);
            return Ok(createOrderRes);
        }

        [Route("updateorder/{orderId}")]
        [HttpPatch]
        [Produces("application/json")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateOrder([FromRoute] int orderId)
        {
            var createOrderRes = await _orderService.UpdateOrder(orderId);
            return Ok(createOrderRes);
        }


    }
}

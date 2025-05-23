using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.OrderDTOs;
using Project.BL.Services.InternalServices.Abstractions;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDTO orderCreateDTO)
        {
            try
            {
                var res = await _orderService.CreateAsync(orderCreateDTO);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _orderService.GetAllAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

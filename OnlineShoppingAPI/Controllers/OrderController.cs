
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private IConfiguration _configuration;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository =orderRepository;
            
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet, Route("GetOrders")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrders();
                return StatusCode(200, orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("GetOrder/{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var order = await _orderRepository.GetOrderById(id);
                if (order != null)
                {
                    return StatusCode(200, order);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Route("AddOrder")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    order.OrderId = Guid.NewGuid();
                    await _orderRepository.Add(order);
                    return StatusCode(200, order);
                }
                else
                {
                    return BadRequest("Enter Valid Details!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("DeleteOrder")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                await _orderRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

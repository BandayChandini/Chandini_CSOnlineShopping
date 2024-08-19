using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderitemRepository;
        private IConfiguration _configuration;
        public OrderItemController(IOrderItemRepository orderitemRepository)
        {
            _orderitemRepository = orderitemRepository;
            
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet, Route("GetOrderItems")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orderitems = await _orderitemRepository.GetAllOrderItems();
                return StatusCode(200, orderitems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet, Route("GetOrderitems/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var orderitems = await _orderitemRepository.GetOrderItemById(id);
                if (orderitems != null)
                {
                    return StatusCode(200, orderitems);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Customer")]
        [HttpPost, Route("AddOrderItem")]
        public async Task<IActionResult> Add([FromBody] OrderItem orderItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderItem.OrderItemId = Guid.NewGuid();
                    await _orderitemRepository.AddOrderItem(orderItem);
                    return StatusCode(200, orderItem);
                }
                else
                {
                    return BadRequest("Enter Valid Details!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}

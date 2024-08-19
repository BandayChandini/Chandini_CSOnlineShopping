using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _cartitemRepository;
        private IConfiguration _configuration;
        public CartItemController(ICartItemRepository cartitemRepository)
        {
              _cartitemRepository = cartitemRepository;
               
            
        }

        [HttpGet, Route("GetAllCartItems")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cartitem = await _cartitemRepository.GetAllCartItems();
                return StatusCode(200, cartitem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("GetCartItem/{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var cartitem = await _cartitemRepository.GetCartItemById(id);
                if (cartitem != null)
                {
                    return StatusCode(200, cartitem);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Route("AddCartItem")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add([FromBody] CartItem cartitem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cartitem.CartItemId = Guid.NewGuid();

                    await _cartitemRepository.AddCartItem(cartitem);
                    return StatusCode(200, cartitem);
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
        [HttpPut, Route("EditCartItem")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Edit([FromBody] CartItem cartitem)
        {
            try
            {
                await _cartitemRepository.UpdateCartItem(cartitem);
                return StatusCode(200, cartitem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Route("DeleteCartItem")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                await _cartitemRepository.DeleteCartItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

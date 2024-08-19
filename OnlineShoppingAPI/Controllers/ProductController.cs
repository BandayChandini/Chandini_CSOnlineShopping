using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private IConfiguration _configuration;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet, Route("GetProducts")]
        
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productRepository.GetAllProducts();
                return StatusCode(200, products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("GetProduct/{id}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);
                if (product != null)
                {
                    return StatusCode(200, product);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Route("AddProduct")]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.ProductId = Guid.NewGuid();
                    await _productRepository.Add(product);
                    return StatusCode(200, product);
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
        [HttpPut, Route("EditProduct")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit([FromBody] Product product)
        {
            try
            {
                await _productRepository.Update(product);
                return StatusCode(200, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Route("DeleteProduct")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                await _productRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

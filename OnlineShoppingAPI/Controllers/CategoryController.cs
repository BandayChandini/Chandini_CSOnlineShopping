using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private IConfiguration _configuration;
        public CategoryController(ICategoryRepository categoryRepository)
        {

            _categoryRepository = categoryRepository;

        }

        [HttpGet, Route("GetAllCategories")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var category = await _categoryRepository.GetAllCategories();
                return StatusCode(200, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetCategory/{id}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if (category != null)
                {
                    return StatusCode(200, category);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("AddCategory")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add([FromBody] Category category)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    category.CategoryId = Guid.NewGuid();
                    await _categoryRepository.AddCategory(category);
                    return StatusCode(200, category);
                }
                else
                {
                    return BadRequest("Enter Valid Details!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut, Route("EditCategory")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.UpdateCategory(category);
                return StatusCode(200, category);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpDelete, Route("DeleteCategory")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                await _categoryRepository.DeleteCategory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

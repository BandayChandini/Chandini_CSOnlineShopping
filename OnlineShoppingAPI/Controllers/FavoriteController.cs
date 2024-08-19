using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private IConfiguration _configuration;
        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            
            _favoriteRepository = favoriteRepository;
            
        }
        [Authorize(Roles = "Admin,Customer")]

        [HttpGet, Route("GetFavorites")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var favorites = await _favoriteRepository.GetAllFavorites();
                return StatusCode(200, favorites);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet, Route("GetFavorites/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var favorite = await _favoriteRepository.GetFavoriteById(id);
                if (favorite != null)
                {
                    return StatusCode(200, favorite);
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
        [HttpPost, Route("AddFavorite")]
        public async Task<IActionResult> Add([FromBody] Favorite favorite)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    favorite.FavoriteId=Guid.NewGuid(); 
                    await _favoriteRepository.Add(favorite);
                    return StatusCode(200, favorite);
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
        [Authorize(Roles = "Customer")]
        [HttpPut, Route("EditFavorite")]
        public async Task<IActionResult> Edit([FromBody] Favorite favorite)
        {
            try
            {
                await _favoriteRepository.Update(favorite);
                return StatusCode(200, favorite);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Route("DeleteFavorite")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                await _favoriteRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

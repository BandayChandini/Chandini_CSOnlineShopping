using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private IConfiguration _configuration;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            
        }
        [HttpGet, Route("GetTransactionByDate/{date}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Get([FromRoute] DateTime date)
        {
            try
            {
                var transaction = await _transactionRepository.GetTransactionByDate(date);
                if (transaction != null)
                {
                    return StatusCode(200, transaction);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Customer")]

        [HttpGet, Route("GetTransactionById/{id}")]
       
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var transaction = await _transactionRepository.GetTransactionById(id);
                if (transaction != null)
                {
                    return StatusCode(200, transaction);
                }
                else
                    return StatusCode(404, "Invalid Id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Route("AddTransaction")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> Add([FromBody] Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    transaction.TransactionId = Guid.NewGuid();
                    await _transactionRepository.AddTransaction(transaction);
                    return StatusCode(200, transaction);
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
       
    }
}

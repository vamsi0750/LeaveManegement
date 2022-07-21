using LeaveManegementApi.Models;
using LeaveManegementApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace LeaveManegementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _account.GetAllUsers();
            if(result == null || result.Count == 0)
            {
                return NoContent();
            }
            return Ok(await _account.GetAllUsers());
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("user-registration")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistration userRegistration)
        {
            var result = await _account.UserRegistration(userRegistration);
            if (result.Contains("Exist"))
            {
                return Conflict(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

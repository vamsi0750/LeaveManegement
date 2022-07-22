using LeaveManegementApi.Models;
using LeaveManegementApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var result = await _account.Login(userLogin);
            if (result.Contains("Found"))
            {
                return NotFound(result);
            }
            else if(result.Contains("Password"))
            {
                return BadRequest(result);
            }
            else if (result.Contains("Verified"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("verify")]
        public async Task<IActionResult> verify([FromQuery] string token)
        {
            var result = await _account.Verify(token);
            if (result.Contains("InValid"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery][EmailAddress] string email)
        {
            var result = await _account.ForgotPassword(email);
            if (result.Contains("Found"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            var result = await _account.ResetPassword(resetPassword);
            if (result.Contains("InValid"))
            {
                return BadRequest(result);
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

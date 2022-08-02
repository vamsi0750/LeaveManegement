using LeaveManegementApi.Models;
using LeaveManegementApi.Repository;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "1")]
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

        [HttpPost("user-registration")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistration userRegistration)
        {
            var result = await _account.UserRegistration(userRegistration);
            if (result.ResponceMessage.Contains("Exist"))
            {
                return Conflict(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var result = await _account.Login(userLogin);
            if (result.ResponceMessage.Contains("Found"))
            {
                return NotFound(result);
            }
            else if(result.ResponceMessage.Contains("Password"))
            {
                return BadRequest(result);
            }
            else if (result.ResponceMessage.Contains("Verified"))
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
            }else if(result.Contains("Something"))
            {
                return BadRequest(result);
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
    }
}

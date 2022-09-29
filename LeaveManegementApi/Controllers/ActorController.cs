using LeaveManegementApi.Repository.Movie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManegementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActor _actor;
        public ActorController(IActor actor)
        {
            _actor = actor;
        }

        [HttpGet("all-actors")]
        public async Task<IActionResult> GetAllActors()
        {
            var movies = await _actor.Actors();
            if(movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}

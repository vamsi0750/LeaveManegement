using LeaveManegementApi.Repository.Movie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManegementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovie _movie;
        public MovieController(IMovie movie)
        {
            _movie = movie;
        }

        [HttpGet("all-movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movie.GetAllMovies();
            if(movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}

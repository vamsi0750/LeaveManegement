using LeaveManegementApi.Repository.Movie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManegementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducer _producer;
        public ProducerController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpGet("all-producer")]
        public async Task<IActionResult> GetAllproducer()
        {
            var movies = await _producer.producers();
            if(movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}

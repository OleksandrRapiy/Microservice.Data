using API.Domain.Models;
using API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/dev/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetAllAsync();

            if (!movies.Any())
                return NotFound();

            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            await _movieRepository.AddAsync(movie);

            return Ok();
        }
    }
}

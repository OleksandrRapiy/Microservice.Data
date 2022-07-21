using MediatR;
using Microservice.Data.Application.Commands;
using Microservice.Data.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservice.Data.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        public readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _mediator.Send(new GetAllMoviesQuery()));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

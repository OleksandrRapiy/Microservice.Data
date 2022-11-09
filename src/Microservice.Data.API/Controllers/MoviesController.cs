using MediatR;
using Microservice.Data.Application.Commands;
using Microservice.Data.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservice.Data.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "microservice.data.api")]
    public class MoviesController : ControllerBase
    {

        public readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Get list of all movies async.
        /// </summary>
        /// <returns>List of all movies.</returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetMoviesAsync()
            => Ok(await _mediator.Send(new GetAllMoviesQuery()));


        /// <summary>
        /// Create movie async.
        /// </summary>
        /// <returns>Empty object if movie was created.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMovieAsync(CreateMovieCommand command)
           => Ok(await _mediator.Send(command));
    }
}

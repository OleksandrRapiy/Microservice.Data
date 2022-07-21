using MediatR;
using Microservice.Data.Application.Interfaces;
using Microservice.Data.Application.Queries;
using Microservice.Data.Domain.Entites;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Data.Infrastructure.Handlers
{
    internal class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieEntity>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieEntity>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _movieRepository.GetAllAsync();
        }
    }
}

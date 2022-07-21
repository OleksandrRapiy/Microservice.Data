using MediatR;
using Microservice.Data.Application.Commands;
using Microservice.Data.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Data.Infrastructure.Handlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Unit>
    {
        public readonly IMovieRepository _movieRepository;

        public CreateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            await _movieRepository.AddAsync(new Domain.Entites.MovieEntity(request.Name, request.Author));

            return Unit.Value;
        }
    }
}

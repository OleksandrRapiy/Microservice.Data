using MediatR;
using Microservice.Data.Application.Commands;
using Microservice.Data.Application.Interfaces;
using Microservice.Data.Domain.Entites;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Data.Infrastructure.Handlers.CommandHandlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Unit>
    {
        public readonly IUnitOfWork _unitOfWork;

        public CreateMovieCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.MovieRepository.AddAsync(new MovieEntity(request.Name, request.Author));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

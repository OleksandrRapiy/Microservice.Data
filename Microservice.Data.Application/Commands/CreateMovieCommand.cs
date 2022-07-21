using MediatR;

namespace Microservice.Data.Application.Commands
{
    public class CreateMovieCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }
}

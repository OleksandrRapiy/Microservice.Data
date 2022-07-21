using MediatR;
using Microservice.Data.Domain.Entites;
using System.Collections.Generic;

namespace Microservice.Data.Application.Queries
{
    public class GetAllMoviesQuery: IRequest<IEnumerable<MovieEntity>>
    {
    }
}

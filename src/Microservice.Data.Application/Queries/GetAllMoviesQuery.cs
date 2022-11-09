using MediatR;
using Microservice.Data.Application.Dtos;
using System.Collections.Generic;

namespace Microservice.Data.Application.Queries
{
    public class GetAllMoviesQuery: IRequest<IEnumerable<MovieDto>>
    {
    }
}

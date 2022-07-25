using AutoMapper;
using MediatR;
using Microservice.Data.Application.Dtos;
using Microservice.Data.Application.Interfaces;
using Microservice.Data.Application.Queries;
using Microservice.Data.Domain.Entites;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Data.Infrastructure.Handlers
{
    internal class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMoviesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<MovieEntity>, IEnumerable<MovieDto>>(await _unitOfWork.MovieRepository.GetAllAsync());
        }
    }
}

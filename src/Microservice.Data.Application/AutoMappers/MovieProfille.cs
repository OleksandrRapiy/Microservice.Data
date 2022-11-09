using AutoMapper;
using Microservice.Data.Application.Dtos;
using Microservice.Data.Domain.Entites;

namespace Microservice.Data.Application.AutoMappers
{
    public class MovieProfille : Profile
    {
        public MovieProfille()
        {
            CreateMap<MovieEntity, MovieDto>()
                .ReverseMap();
        }
    }
}

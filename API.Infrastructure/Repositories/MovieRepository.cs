using API.Domain.Models;
using API.Infrastructure.Context;
using API.Infrastructure.Repositories.Base;

namespace API.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApiDbContext context) : base(context)
        {

        }
    }
}

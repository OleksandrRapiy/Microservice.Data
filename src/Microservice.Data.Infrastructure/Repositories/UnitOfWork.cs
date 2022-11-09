using Microservice.Data.Application.Interfaces;
using Microservice.Data.Domain.Entites;
using Microservice.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Data.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        private IRepository<MovieEntity> _movieRepository;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<MovieEntity> MovieRepository => _movieRepository ??= new MovieRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

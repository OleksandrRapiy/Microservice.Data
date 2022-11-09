using Microservice.Data.Domain.Entites;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Data.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<MovieEntity> MovieRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

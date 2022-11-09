using Microservice.Data.Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice.Data.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
    }
}

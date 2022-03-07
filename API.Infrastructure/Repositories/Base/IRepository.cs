using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Infrastructure.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
    }
}

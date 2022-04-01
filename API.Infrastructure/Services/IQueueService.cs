using Azure.Storage.Queues.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Infrastructure.Services
{
    public interface IQueueService
    {
        Task AddAsync(string message);
    }
}

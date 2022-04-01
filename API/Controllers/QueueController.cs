using API.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/dev/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string message)
        {
            await _queueService.AddAsync(message);

            return Ok();
        }
    }
}

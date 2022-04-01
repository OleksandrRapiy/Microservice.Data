using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}

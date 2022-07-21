using Microservice.Data.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Data.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MovieEntity> Movies { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
    }
}

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Infrastructure.Context
{
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            // Build config
            IConfiguration lConfigurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("migration.json")
                .Build();

            return CreateDbContext(lConfigurationBuilder);
        }

        public static ApiDbContext CreateDbContext(IConfiguration configuration)
        {
            string lConnectionString = configuration.GetConnectionString(nameof(DbContext));

            DbContextOptionsBuilder<ApiDbContext> builder = new DbContextOptionsBuilder<ApiDbContext>();
            builder.UseSqlServer(lConnectionString);

            return new ApiDbContext(builder.Options);
        }
    }
}

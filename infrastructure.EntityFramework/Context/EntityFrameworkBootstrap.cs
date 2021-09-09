using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.EntityFramework.Context
{
    public static class EntityFrameworkBootstrap
    {
        public static void AddSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlite");

            services.AddDbContext<SqliteContext>(
                dbContextOptions => dbContextOptions
                    .UseSqlite(connectionString)
            );
        }
    }
}
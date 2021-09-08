using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.EntityFramework.Context
{
    public class EntityFrameworkBootstrap
    {
        public static void AddSqlite(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlite");
            
            services.AddDbContext<SqliteContext>(
                dbContextOptions => dbContextOptions
                    .UseSqlite(connectionString)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                , ServiceLifetime.Singleton);
        }
    }
}
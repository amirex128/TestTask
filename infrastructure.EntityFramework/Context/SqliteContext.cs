using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.EntityFramework.Context
{
    public class SqliteContext:DbContext
    {
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<User> Users { get; set; }
        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
        {

        }
        
    }
}   
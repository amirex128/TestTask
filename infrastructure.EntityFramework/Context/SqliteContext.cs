﻿using Microsoft.EntityFrameworkCore;

namespace infrastructure.EntityFramework.Context
{
    public class SqliteContext:DbContext
    {
        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
 
            modelBuilder.Entity<SqliteContext>()
                .HasKey(p => new {  });
        } 
    }
}   
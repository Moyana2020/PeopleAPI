
using Abstractions;
using Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

using System.Text;

namespace Infrastructure.SQL
{
    public class SQLContext : DbContext
    {

        private IOptions<AppSettings> _config;

        public SQLContext(IOptions<AppSettings> config)
        {
            _config = config;
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.Value.SqlServerName);
        }

        public DbSet<PersonEntity> People { get; set; }
    }
}

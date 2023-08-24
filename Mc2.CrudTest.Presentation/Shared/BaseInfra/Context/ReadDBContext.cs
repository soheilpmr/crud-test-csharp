using Mc2.CrudTest.Presentation.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Context
{
    public class ReadDBContext : DbContext
    {
        public ReadDBContext(DbContextOptions<WriteDBContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }
        public ReadDBContext() : base()
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<CustomerEntity> CustomerEntities { get; set; }

        private static string getConnectionString()
        {
            var environmentName =
              Environment.GetEnvironmentVariable(
                  "ASPNETCORE_ENVIRONMENT");

            //Console.WriteLine("2");
            var config = new ConfigurationBuilder().AddJsonFile("appsettings" + (String.IsNullOrWhiteSpace(environmentName) ? "" : "." + environmentName) + ".json", false).Build();

            return config.GetConnectionString("DefaultConnection");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Console.WriteLine("4");
            optionsBuilder.UseSqlServer(getConnectionString());
        }
    }
}

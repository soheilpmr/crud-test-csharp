using Mc2.CrudTest.Presentation.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Client.Context
{
    public class ReadDBContext : DbContext
    {
        public ReadDBContext(DbContextOptions<ReadDBContext> options) : base(options)
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
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings" + (String.IsNullOrWhiteSpace(environmentName) ? "" : "." + environmentName) + ".json", false).Build();

            //return config.GetConnectionString("DefaultConnection");
            return "";
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Console.WriteLine("4");
        //    optionsBuilder.UseSqlServer(getConnectionString());
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.Property(e => e.DateOfBirth)
                      .HasColumnName("DateOfBirth")
                      .HasColumnType("date");

            });
        }
    }
}

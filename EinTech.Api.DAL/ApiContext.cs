using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EinTech.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EinTech.Api.DAL
{
    public class ApiContext : DbContext
    {
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }

        public ApiContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.SeedInitialData(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateCreatedAndUpdateDate();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateCreatedAndUpdateDate();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateCreatedAndUpdateDate()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity) entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity) entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../EinTech/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApiContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlite(connectionString);
            return new ApiContext(builder.Options);
        }
    }
}

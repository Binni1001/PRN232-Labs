using Microsoft.EntityFrameworkCore;
using ValidationAndRouting.Entities.Configuration;
using ValidationAndRouting.Models;

namespace ValidationAndRouting.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using ChainOfResponsibility.Entities;

namespace ChainOfResponsibility.DataAccess
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=localhost\SQLEXPRESS;
                  Initial Catalog=ChainOfResponsibilityDb;
                  Integrated Security=True;
                  TrustServerCertificate=True;");
        }

        public DbSet<CustomerProcess> CustomerProcesses { get; set; }
    }
}
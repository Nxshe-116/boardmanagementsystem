using BoardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BoardManagementSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<DocumentDetail> Documents { get; set; }

        public DbSet<User> TeloneUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<TeloneUserRole> TeloneUserRoles { get; set; }
    }
}

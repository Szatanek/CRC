using CRC.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CRC.Repository.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleSimply> RolesSimplies { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ProvisionedPermission> ProvisionedPermissions { get; set; }
    }
}

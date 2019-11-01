using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Context : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Group Permission relationship
            modelBuilder.Entity<GroupPermission>()
                .HasKey(x => new { x.GroupId, x.PermissionId });
            
            modelBuilder.Entity<GroupPermission>()
                .HasOne(g => g.Group)
                .WithMany(p => p.Permissions)
                .HasForeignKey(x => x.GroupId);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(p => p.Permission)
                .WithMany(g => g.Groups)
                .HasForeignKey(x => x.PermissionId);

            // Group User relationship
            modelBuilder.Entity<GroupUser>()
                .HasKey(x => new { x.GroupId, x.UserId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(g => g.Group)
                .WithMany(u => u.Users)
                .HasForeignKey(x => x.GroupId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(u => u.User)
                .WithMany(g => g.Groups)
                .HasForeignKey(x => x.UserId);
        }
    }
}

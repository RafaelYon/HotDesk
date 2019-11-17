using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class Context : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email).IsUnique();

			// GroupPermission relationship
			modelBuilder.Entity<GroupPermission>()
				.HasKey(x => new { x.GroupId, x.Permission });

			modelBuilder.Entity<GroupPermission>()
				.HasOne(gp => gp.Group)
				.WithMany(g => g.GroupPermissions)
				.HasForeignKey(gp => gp.GroupId);

            // Group User relationship
            modelBuilder.Entity<GroupUser>()
                .HasKey(x => new { x.GroupId, x.UserId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUser)
                .HasForeignKey(gu => gu.GroupId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.GroupUser)
                .HasForeignKey(gu => gu.UserId);
		}
	}
}

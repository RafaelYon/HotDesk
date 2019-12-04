using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Repository
{
	public class Context : DbContext
    {
		public DbSet<Permission> Permissions { get; set; }

		public DbSet<Group> Groups { get; set; }

		public DbSet<GroupPermission> GroupPermissions { get; set; }

		public DbSet<User> Users { get; set; }

        public DbSet<GroupUser> GroupUser { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Group>()
                .HasIndex(x => x.Name).IsUnique();

            // GroupPermission relationship
            modelBuilder.Entity<GroupPermission>()
				.HasKey(x => new { x.GroupId, x.PermissionId });

			modelBuilder.Entity<GroupPermission>()
				.HasOne(gp => gp.Group)
				.WithMany(g => g.GroupPermissions)
				.HasForeignKey(gp => gp.GroupId);

			modelBuilder.Entity<GroupPermission>()
				.HasOne(gp => gp.Permission)
				.WithMany(p => p.GroupPermissions)
				.HasForeignKey(gp => gp.PermissionId);

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

			// User Issue relationship
			modelBuilder.Entity<User>()
				.HasMany(u => u.IssuesCreated)
				.WithOne(i => i.Owner)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasMany(u => u.IssuesAssigned)
				.WithOne(i => i.Responsible)
				.OnDelete(DeleteBehavior.Restrict);

			// Issue IssueComment relationship
			modelBuilder.Entity<Issue>()
				.HasMany(i => i.Comments)
				.WithOne(c => c.Issue)
				.OnDelete(DeleteBehavior.Restrict);

			// Seding data
			modelBuilder.Entity<Permission>().HasData((new Permission()).GetSeedData().ToArray());
			modelBuilder.Entity<Group>().HasData((new Group()).GetSeedData().ToArray());
			modelBuilder.Entity<GroupPermission>().HasData((new GroupPermission()).GetSeedData().ToArray());

            modelBuilder.Entity<User>().HasData((new User()).GetSeedData().ToArray());
            modelBuilder.Entity<GroupUser>().HasData((new GroupUser()).GetSeedData().ToArray());
        }
	}
}

using Domain;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
                .HasOne(gp => gp.Group)
                .WithMany(g => g.GroupPermission)
                .HasForeignKey(gp => gp.GroupId);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Permission)
                .WithMany(p => p.GroupPermission)
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

			// Seeding data
			List<Permission> permissions = new List<Permission>();

			foreach (PermissionTypes permissionType in (PermissionTypes[])Enum.GetValues(typeof(PermissionTypes)))
			{
				permissions.Add(new Permission
				{
					Id = (int) permissionType,
					Name = permissionType.ToString(),
					Description = permissionType.GetFieldAttribute<DisplayAttribute>().Description
				});
			}

			modelBuilder.Entity<Permission>().HasData(permissions.ToArray());
		}
	}
}

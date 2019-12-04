﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(IssueeDAO))]
    [Migration("20191125003625_CreatedFirstDataStruct")]
    partial class CreatedFirstDataStruct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Default");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Groups");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Default = true, Name = "Default", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Default = false, Name = "Support", UpdatedAt = new DateTime(2019, 11, 24, 14, 24, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 3, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Default = false, Name = "Admin", UpdatedAt = new DateTime(2019, 11, 24, 14, 24, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("Domain.GroupPermission", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("PermissionId");

                    b.HasKey("GroupId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermissions");

                    b.HasData(
                        new { GroupId = 1, PermissionId = 1 },
                        new { GroupId = 1, PermissionId = 3 },
                        new { GroupId = 1, PermissionId = 4 },
                        new { GroupId = 2, PermissionId = 1 },
                        new { GroupId = 2, PermissionId = 2 },
                        new { GroupId = 2, PermissionId = 3 },
                        new { GroupId = 3, PermissionId = 1 },
                        new { GroupId = 3, PermissionId = 2 },
                        new { GroupId = 3, PermissionId = 3 },
                        new { GroupId = 3, PermissionId = 5 },
                        new { GroupId = 3, PermissionId = 6 },
                        new { GroupId = 3, PermissionId = 7 }
                    );
                });

            modelBuilder.Entity("Domain.GroupUser", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("UserId");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("Domain.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int?>("OwnerId");

                    b.Property<float?>("Rate");

                    b.Property<int?>("ResponsibleId");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ResponsibleId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Domain.IssuesComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedById");

                    b.Property<int>("IssueId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("IssueId");

                    b.ToTable("IssuesComment");
                });

            modelBuilder.Entity("Domain.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "IssueCreate", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "IssueAccept", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 3, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "IssueClose", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 4, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "IssueRateAssistence", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 5, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "ManageAccounts", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 6, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "ManageGroups", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 7, CreatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified), Name = "ManageCategories", UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.GroupPermission", b =>
                {
                    b.HasOne("Domain.Group", "Group")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Permission", "Permission")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.GroupUser", b =>
                {
                    b.HasOne("Domain.Group", "Group")
                        .WithMany("GroupUser")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.User", "User")
                        .WithMany("GroupUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Issue", b =>
                {
                    b.HasOne("Domain.Category", "Category")
                        .WithMany("Issues")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.User", "Owner")
                        .WithMany("IssuesCreated")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.User", "Responsible")
                        .WithMany("IssuesAssigned")
                        .HasForeignKey("ResponsibleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Domain.IssuesComment", b =>
                {
                    b.HasOne("Domain.User", "CreatedBy")
                        .WithMany("IssuesComments")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Issue", "Issue")
                        .WithMany("Comments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

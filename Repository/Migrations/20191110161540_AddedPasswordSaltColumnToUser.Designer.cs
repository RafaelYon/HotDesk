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
    [DbContext(typeof(Context))]
    [Migration("20191110161540_AddedPasswordSaltColumnToUser")]
    partial class AddedPasswordSaltColumnToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Domain.GroupPermission", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("PermissionId");

                    b.HasKey("GroupId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermission");
                });

            modelBuilder.Entity("Domain.GroupUser", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("UserId");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("Domain.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Permissions");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 361, DateTimeKind.Local), Description = "Criar chamado", Name = "IssueCreate", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 368, DateTimeKind.Local) },
                        new { Id = 2, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local), Description = "Assumir chamado", Name = "IssueAccept", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local) },
                        new { Id = 3, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local), Description = "Encerrar chamado", Name = "IssueClose", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local) },
                        new { Id = 4, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local), Description = "Escalar chamado", Name = "IssueEscalate", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local) },
                        new { Id = 5, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local), Description = "Avaliar assitencia", Name = "IssueRateAssistence", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local) },
                        new { Id = 6, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local), Description = "Gerenciar contas", Name = "ManageAccounts", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local) },
                        new { Id = 7, CreatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local), Description = "Gerenciar grupos", Name = "ManageGroups", UpdatedAt = new DateTime(2019, 11, 10, 14, 15, 39, 369, DateTimeKind.Local) }
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

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PasswordSalt")
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
                        .WithMany("GroupPermission")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Permission", "Permission")
                        .WithMany("GroupPermission")
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
#pragma warning restore 612, 618
        }
    }
}

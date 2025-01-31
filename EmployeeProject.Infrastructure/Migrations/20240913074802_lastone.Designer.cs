﻿// <auto-generated />
using System;
using EmployeeProject.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240913074802_lastone")]
    partial class lastone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<string>("ActivityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.AllowedHours", b =>
                {
                    b.Property<int>("AllowedHoursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AllowedHoursId"));

                    b.Property<float>("Number")
                        .HasColumnType("real");

                    b.HasKey("AllowedHoursId");

                    b.ToTable("AllowedHours");

                    b.HasData(
                        new
                        {
                            AllowedHoursId = 1,
                            Number = 0.5f
                        },
                        new
                        {
                            AllowedHoursId = 2,
                            Number = 1f
                        },
                        new
                        {
                            AllowedHoursId = 3,
                            Number = 1.5f
                        },
                        new
                        {
                            AllowedHoursId = 4,
                            Number = 2f
                        },
                        new
                        {
                            AllowedHoursId = 5,
                            Number = 2.5f
                        },
                        new
                        {
                            AllowedHoursId = 6,
                            Number = 3f
                        },
                        new
                        {
                            AllowedHoursId = 7,
                            Number = 3.5f
                        },
                        new
                        {
                            AllowedHoursId = 8,
                            Number = 4f
                        },
                        new
                        {
                            AllowedHoursId = 9,
                            Number = 4.5f
                        },
                        new
                        {
                            AllowedHoursId = 10,
                            Number = 5f
                        },
                        new
                        {
                            AllowedHoursId = 11,
                            Number = 5.5f
                        },
                        new
                        {
                            AllowedHoursId = 12,
                            Number = 6f
                        },
                        new
                        {
                            AllowedHoursId = 13,
                            Number = 6.5f
                        },
                        new
                        {
                            AllowedHoursId = 14,
                            Number = 7f
                        },
                        new
                        {
                            AllowedHoursId = 15,
                            Number = 7.5f
                        },
                        new
                        {
                            AllowedHoursId = 16,
                            Number = 8f
                        },
                        new
                        {
                            AllowedHoursId = 17,
                            Number = 8.5f
                        });
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.BusinessOrIt", b =>
                {
                    b.Property<int>("BusinessOrItId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessOrItId"));

                    b.Property<string>("BusinessOrItName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusinessOrItId");

                    b.ToTable("BusinessOrIts");

                    b.HasData(
                        new
                        {
                            BusinessOrItId = 1,
                            BusinessOrItName = "Business"
                        },
                        new
                        {
                            BusinessOrItId = 2,
                            BusinessOrItName = "IT"
                        });
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.DataSheetBus", b =>
                {
                    b.Property<int>("DataSheetBusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DataSheetBusId"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BusinessOrItId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<float>("HoursPerDay")
                        .HasColumnType("real");

                    b.Property<double>("HoursPerMonth")
                        .HasColumnType("float");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("SectionId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("DataSheetBusId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("BusinessOrItId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SectionId");

                    b.ToTable("DataSheetBuses");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.HolidayStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("HolidayStatus");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusName = "fixed"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusName = "movable"
                        });
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Holidays", b =>
                {
                    b.Property<int>("FixedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FixedId"));

                    b.Property<DateOnly>("FixedDate")
                        .HasColumnType("date");

                    b.Property<string>("FixedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HolidayStatusId")
                        .HasColumnType("int");

                    b.HasKey("FixedId");

                    b.HasIndex("HolidayStatusId");

                    b.ToTable("Holidays");

                    b.HasData(
                        new
                        {
                            FixedId = 1,
                            FixedDate = new DateOnly(2024, 1, 1),
                            FixedName = "New Year's Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 2,
                            FixedDate = new DateOnly(2024, 2, 24),
                            FixedName = "Cebu Charter",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 3,
                            FixedDate = new DateOnly(2024, 2, 25),
                            FixedName = "Edsa",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 4,
                            FixedDate = new DateOnly(2024, 4, 9),
                            FixedName = "Araw ng Kagitingan",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 5,
                            FixedDate = new DateOnly(2024, 5, 1),
                            FixedName = "Labor Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 6,
                            FixedDate = new DateOnly(2024, 6, 12),
                            FixedName = "Independence Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 7,
                            FixedDate = new DateOnly(2024, 8, 6),
                            FixedName = "Cebu Provincial Charter Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 8,
                            FixedDate = new DateOnly(2024, 8, 21),
                            FixedName = "Ninoy Aquino",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 9,
                            FixedDate = new DateOnly(2024, 9, 9),
                            FixedName = "Osmeña Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 10,
                            FixedDate = new DateOnly(2024, 11, 1),
                            FixedName = "All Saint's Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 11,
                            FixedDate = new DateOnly(2024, 11, 2),
                            FixedName = "All Souls' Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 12,
                            FixedDate = new DateOnly(2024, 11, 30),
                            FixedName = "Bonifacio Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 13,
                            FixedDate = new DateOnly(2024, 12, 8),
                            FixedName = "Immaculate Conception",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 14,
                            FixedDate = new DateOnly(2024, 12, 24),
                            FixedName = "Christmas Eve",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 15,
                            FixedDate = new DateOnly(2024, 12, 25),
                            FixedName = "Christmas Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 16,
                            FixedDate = new DateOnly(2024, 12, 30),
                            FixedName = "Rizal Day",
                            HolidayStatusId = 1
                        },
                        new
                        {
                            FixedId = 17,
                            FixedDate = new DateOnly(2024, 12, 31),
                            FixedName = "New Year's Eve",
                            HolidayStatusId = 1
                        });
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Legend", b =>
                {
                    b.Property<int>("LegendId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LegendId"));

                    b.Property<int?>("LOW")
                        .HasColumnType("int");

                    b.Property<int?>("MAX")
                        .HasColumnType("int");

                    b.Property<int?>("MED")
                        .HasColumnType("int");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("LegendId");

                    b.HasIndex("SectionId");

                    b.ToTable("Legends");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.MovableNames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovableNames");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Additional Special Non-Working"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Chinese New Year"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Maunday Thursday "
                        },
                        new
                        {
                            Id = 4,
                            Name = "Good Friday"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Black Saturday"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Eid al-Fitr"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Eid al-Adha"
                        },
                        new
                        {
                            Id = 8,
                            Name = "National Heroes' Day"
                        });
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectionId"));

                    b.Property<string>("SectionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectionId");

                    b.ToTable("Sections");

                    b.HasData(
                        new
                        {
                            SectionId = 1,
                            SectionName = "Developer"
                        });
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.User.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("isNewUser")
                        .HasColumnType("bit");

                    b.Property<string>("position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profileString")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SectionId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fb54dcbd-55eb-46dd-bd5f-f52acd2f4966",
                            Email = "jcaso@princeretail.com",
                            EmailConfirmed = false,
                            FullName = "Joselito Caso",
                            LockoutEnabled = false,
                            NormalizedEmail = "JCASO@PRINCERETAIL.COM",
                            NormalizedUserName = "JCASO@PRINCERETAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKFrBQwE/iSKo2TzygHyPTzHo2UHIxKKiMZVjeXvuQ0pRWvQr1MBySp7bTNYiN+pKg==",
                            PhoneNumberConfirmed = false,
                            SectionId = 1,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "jcaso@princeretail.com",
                            position = "Manager"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Project_Manager",
                            NormalizedName = "PROJECT_MANAGER"
                        },
                        new
                        {
                            Id = "4",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            RoleId = "2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.DataSheetBus", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.Model.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeProject.Core.Entities.User.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeProject.Core.Entities.Model.BusinessOrIt", "BusinessOrIt")
                        .WithMany()
                        .HasForeignKey("BusinessOrItId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeProject.Core.Entities.Model.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeProject.Core.Entities.Model.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.Navigation("Activity");

                    b.Navigation("AppUser");

                    b.Navigation("BusinessOrIt");

                    b.Navigation("Project");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Holidays", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.Model.HolidayStatus", "HolidayStatus")
                        .WithMany()
                        .HasForeignKey("HolidayStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HolidayStatus");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.Model.Legend", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.Model.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("EmployeeProject.Core.Entities.User.AppUser", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.Model.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeProject.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EmployeeProject.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

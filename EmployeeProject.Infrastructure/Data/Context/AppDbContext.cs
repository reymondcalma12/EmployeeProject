using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace EmployeeProject.Infrastructure.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<AllowedHours> AllowedHours { get; set; }
        public DbSet<BusinessOrIt> BusinessOrIts { get; set; }
        public DbSet<DataSheetBus> DataSheetBuses { get; set; }
        public DbSet<Holidays> Holidays { get; set; }
        public DbSet<Legend> Legends { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<MovableNames> MovableNames { get; set; }
        public DbSet<HolidayStatus> HolidayStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";

            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
            new AppUser{
                Id = ADMIN_ID,
                UserName = "reymondcalma12",
                NormalizedUserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin123#"),
                SecurityStamp = string.Empty
            });



            modelBuilder.Entity<AllowedHours>().HasData(
                new AllowedHours { AllowedHoursId = 1, Number = 0.5f },
                new AllowedHours { AllowedHoursId = 2, Number = 1 },
                new AllowedHours { AllowedHoursId = 3, Number = 1.5f },
                new AllowedHours { AllowedHoursId = 4, Number = 2 },
                new AllowedHours { AllowedHoursId = 5, Number = 2.5f },
                new AllowedHours { AllowedHoursId = 6, Number = 3 },
                new AllowedHours { AllowedHoursId = 7, Number = 3.5f },
                new AllowedHours { AllowedHoursId = 8, Number = 4 },
                new AllowedHours { AllowedHoursId = 9, Number = 4.5f },
                new AllowedHours { AllowedHoursId = 10, Number = 5 },
                new AllowedHours { AllowedHoursId = 11, Number = 5.5f },
                new AllowedHours { AllowedHoursId = 12, Number = 6 },
                new AllowedHours { AllowedHoursId = 13, Number = 6.5f },
                new AllowedHours { AllowedHoursId = 14, Number = 7 },
                new AllowedHours { AllowedHoursId = 15, Number = 7.5f },
                new AllowedHours { AllowedHoursId = 16, Number = 8 },
                  new AllowedHours { AllowedHoursId = 17, Number = 8.5f }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Employee", NormalizedName = "EMPLOYEE" },
                new { Id = "2", Name = "Manager", NormalizedName = "MANAGER" }
            );

            modelBuilder.Entity<HolidayStatus>().HasData(
                new HolidayStatus { StatusId = 1, StatusName = "fixed" },
                new HolidayStatus { StatusId = 2, StatusName = "movable" }
            );


            modelBuilder.Entity<Holidays>().HasData(
                new Holidays { FixedId = 1, FixedName = "New Year's Day", FixedDate = new DateOnly(2024, 1, 1), HolidayStatusId = 1 },
                new Holidays { FixedId = 2, FixedName = "Cebu Charter", FixedDate = new DateOnly(2024, 2, 24), HolidayStatusId = 1 },

                new Holidays { FixedId = 3, FixedName = "Edsa", FixedDate = new DateOnly(2024, 2, 25), HolidayStatusId = 1 },
                new Holidays { FixedId = 4, FixedName = "Araw ng Kagitingan", FixedDate = new DateOnly(2024, 4, 9), HolidayStatusId = 1 },

                new Holidays { FixedId = 5, FixedName = "Labor Day", FixedDate = new DateOnly(2024, 5, 1), HolidayStatusId = 1 },
                new Holidays { FixedId = 6, FixedName = "Independence Day", FixedDate = new DateOnly(2024, 6, 12), HolidayStatusId = 1 },

                new Holidays { FixedId = 7, FixedName = "Cebu Provincial Charter Day", FixedDate = new DateOnly(2024, 8, 6) , HolidayStatusId = 1 },
                new Holidays { FixedId = 8, FixedName = "Ninoy Aquino", FixedDate = new DateOnly(2024, 8, 21) , HolidayStatusId = 1 },

                new Holidays { FixedId = 9, FixedName = "Osmeña Day", FixedDate = new DateOnly(2024, 9, 9) , HolidayStatusId = 1 },
                new Holidays { FixedId = 10, FixedName = "All Saint's Day", FixedDate = new DateOnly(2024, 11, 1) , HolidayStatusId = 1 },

                new Holidays { FixedId = 11, FixedName = "All Souls' Day", FixedDate = new DateOnly(2024, 11, 02) , HolidayStatusId = 1 },
                new Holidays { FixedId = 12, FixedName = "Bonifacio Day", FixedDate = new DateOnly(2024, 11, 30) , HolidayStatusId = 1 },

                new Holidays { FixedId = 13, FixedName = "Immaculate Conception", FixedDate = new DateOnly(2024, 12, 8) , HolidayStatusId = 1 },
                new Holidays { FixedId = 14, FixedName = "Christmas Eve", FixedDate = new DateOnly(2024, 12, 24) , HolidayStatusId = 1 },

                new Holidays { FixedId = 15, FixedName = "Christmas Day", FixedDate = new DateOnly(2024, 12, 25) , HolidayStatusId = 1 },
                new Holidays { FixedId = 16, FixedName = "Rizal Day", FixedDate = new DateOnly(2024, 12, 30) , HolidayStatusId = 1 },

                new Holidays { FixedId = 17, FixedName = "New Year's Eve", FixedDate = new DateOnly(2024, 12, 31) , HolidayStatusId = 1 }
            );

        }
    }
}

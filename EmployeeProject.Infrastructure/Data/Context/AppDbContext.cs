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

            const string ManagerId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";

            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<Section>().HasData(
                new Section { SectionId = 1, SectionName = "Developer" }
            );


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
                new AllowedHours { AllowedHoursId = 16, Number = 8 }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Employee", NormalizedName = "EMPLOYEE" },
                new { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
                new { Id = "3", Name = "Project_Manager", NormalizedName = "PROJECT_MANAGER" },
                new { Id = "4", Name = "Admin", NormalizedName = "ADMIN" }

            );

            modelBuilder.Entity<HolidayStatus>().HasData(
                new HolidayStatus { StatusId = 1, StatusName = "fixed" },
                new HolidayStatus { StatusId = 2, StatusName = "movable" }
            );

            modelBuilder.Entity<BusinessOrIt>().HasData(
                 new BusinessOrIt { BusinessOrItId = 1, BusinessOrItName = "Business" },
                 new BusinessOrIt { BusinessOrItId = 2, BusinessOrItName = "IT" }
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


            modelBuilder.Entity<MovableNames>().HasData(
                new MovableNames { Id = 1, Name = "Additional Special Non-Working" },
                new MovableNames { Id = 2, Name = "Chinese New Year" },

                new MovableNames { Id = 3, Name = "Maunday Thursday " },
                new MovableNames { Id = 4, Name = "Good Friday" },

                new MovableNames { Id = 5, Name = "Black Saturday" },
                new MovableNames { Id = 6, Name = "Eid al-Fitr" },

                new MovableNames { Id = 7, Name = "Eid al-Adha" },
                new MovableNames { Id = 8, Name = "National Heroes' Day" }

            );


            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        UserId = ADMIN_ID,
            //        RoleId = "4"
            //    }
            //);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = ManagerId,
                    RoleId = "2"
                }
            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = ManagerId,
                    UserName = "jcaso@princeretail.com",
                    NormalizedUserName = "JCASO@PRINCERETAIL.COM",
                    Email = "jcaso@princeretail.com",
                    NormalizedEmail = "JCASO@PRINCERETAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "1234578"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    position = "Manager",
                    SectionId = 1,
                    FullName = "Joselito Caso",
                });

            //modelBuilder.Entity<AppUser>().HasData(
            //    new AppUser
            //    {
            //        Id = ADMIN_ID,
            //        UserName = "ictdev@princeretail.com",
            //        NormalizedUserName = "ICTDEV@PRINCERETAIL.COM",
            //        Email = "ictdev@princeretail.com",
            //        NormalizedEmail = "ICTDEV@PRINCERETAIL.COM",
            //        EmailConfirmed = false,
            //        PasswordHash = hasher.HashPassword(null, "1234578"),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        position = "Admin",
            //        SectionId = null,
            //        FullName = "ICT DEV",
            //    });

        }
    }
}

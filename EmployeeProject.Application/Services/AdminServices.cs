using EmployeeProject.Application.DTOs.Admin;
using EmployeeProject.Application.Interfaces;
using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;
using EmployeeProject.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeProject.Application.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAppUnitOfWork appUnitOfWork;
        private readonly IHttpContextAccessor httpcontext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminServices(UserManager<AppUser> userManager,
                              IAppUnitOfWork appUnitOfWork,
                              IHttpContextAccessor httpcontext,
                              IWebHostEnvironment webHostEnvironment,
                              RoleManager<IdentityRole> roleManager)
        {

            this.userManager = userManager;
            this.appUnitOfWork = appUnitOfWork;
            this.httpcontext = httpcontext;
            this.webHostEnvironment = webHostEnvironment;   
            this.roleManager = roleManager; 
        }

        public async Task<bool> UpdateProfile(IFormFile image)
        {

            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            if (!string.IsNullOrEmpty(userId))
            {

                var user = userManager.Users.FirstOrDefault(x => x.Id == userId);

                var profileName = Guid.NewGuid().ToString() + image.FileName;
                var imageFilePath = Path.Combine(webHostEnvironment.WebRootPath, "profile", profileName);

                using (var fileStream = new FileStream(imageFilePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                user.profileString = profileName;

                await appUnitOfWork.Complete();
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> AddProject(string name)
        {

            Project project = new Project
            {
                ProjectName = name,
                Status = "ACTIVE"
            };

            appUnitOfWork.repository<Project>().Add(project);

            await appUnitOfWork.Complete();

            return true;

        }

        public async Task<bool> AddActivity(string name)
        {

            Activity activity = new Activity
            {
                ActivityName = name
            };

            appUnitOfWork.repository<Activity>().Add(activity);

            await appUnitOfWork.Complete();

            return true;

        }


        public async Task<IEnumerable<AppUser>> GetUserSearch(string? name)
        {
            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault().ToString();

            if (userRole == "Manager")
            {
                string searchName = name.ToLower();

                var users = await userManager.Users.Include(a => a.Section).Where(a => a.FullName.ToLower()
                   .Contains(searchName))
                    .Where(a => a.SectionId == user.SectionId)
                         .Where(a => a.deActivated != true)
                   .OrderBy(a => a.Section).ToListAsync();
                return users;

            }
            else
            {
                string searchName = name.ToLower();

                var users = await userManager.Users.Include(a => a.Section).Where(a => a.FullName.ToLower()
                   .Contains(searchName))
                         .Where(a => a.deActivated != true)
                   .OrderBy(a => a.Section).ToListAsync();
                return users;

            }
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {

            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault().ToString();

            if (userRole == "Manager")
            {

                var users = await userManager.Users.Include(a => a.Section)
                    .Where(a => a.SectionId == user.SectionId)
                    .Where(a => a.deActivated != true)
                   .OrderBy(a => a.Section).ToListAsync();
                return users;

            }
            else
            {
                var users = await userManager.Users.Include(a => a.Section)
                   .Where(a => a.deActivated != true)
                   .OrderBy(a => a.Section).ToListAsync();
                return users;

            }

        }

        public Task<IEnumerable<Section>> GetSections()
        {    
            var sections = appUnitOfWork.repository<Section>().GetAllAsync();

            return sections;
        }




        public async Task<List<List<SectionForAddLegendDTO>>> GetLegendSections()
        {
            var sections = await appUnitOfWork.repository<Section>().AsQueryable().ToListAsync();
            var legends = await appUnitOfWork.repository<Legend>().AsQueryable().Include(l => l.Section).ToListAsync();

            var sectionDtosList = new List<List<SectionForAddLegendDTO>>();

            foreach (var section in sections)
            {
                var sectionDtos = new List<SectionForAddLegendDTO>
                {
                    new SectionForAddLegendDTO
                    {
                        SectionId = section.SectionId,
                        SectionName = section.SectionName,
                        Status = legends.Any(l => l.SectionId == section.SectionId) ? "disabled" : "null"
                    }
                };

                sectionDtosList.Add(sectionDtos);
            }

            return sectionDtosList;
        }




  


        public async Task<IEnumerable<AppUser>> GetUserAddDataSheet(string name)
        {

            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();
            var user = await userManager.Users.Include(a => a.Section).FirstOrDefaultAsync(a => a.Id == userId);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            var userRoleString = userRole.ToString();

            string searchName = name.ToLower();

            if (userRoleString == "Manager")
            {
                var users = await userManager.Users.Include(a => a.Section).Where(a => a.FullName.ToLower()
                   .Contains(searchName)).Where(a => a.SectionId == user.SectionId).ToListAsync();
                
                return users;
            }
            else
            {
                var users = await userManager.Users.Include(a => a.Section).Where(a => a.FullName.ToLower()
                   .Contains(searchName)).ToListAsync();

                return users;
            }

        
        }



        public async Task<IEnumerable<AppUser>> GetUserAddNewDataForSectionOnly(string name)
        {
            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();
            var user = await userManager.Users.Include(a => a.Section).FirstOrDefaultAsync(a => a.Id == userId);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault().ToString();

            string searchName = name.ToLower();

            if (userRole == "Manager")
            {
                var users = await userManager.Users.Include(a => a.Section).Where(a => a.FullName.ToLower()
                .Contains(searchName)).Where(a => a.deActivated != true).Where(a => a.SectionId == user.SectionId).ToListAsync();

                return users;
            }
            else
            {
                var users = await userManager.Users.Include(a => a.Section).Where(a => a.deActivated != true).Where(a => a.FullName.ToLower()
                .Contains(searchName)).ToListAsync();

                return users;
            }


    
        }

        public async Task<(bool, string, string)> AddDataSheetUserExistEnableButton(string name)
        {

            var userssId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var userss = await userManager.Users.Include(a => a.Section).FirstOrDefaultAsync(a => a.Id == userssId);

            var nameLow = name.ToLower();

            var users = await userManager.Users.Where(u => u.FullName.ToLower().Contains(nameLow)).Include(a => a.Section).Where(a => a.SectionId == userss.SectionId).Where(a => a.deActivated != true).ToListAsync();

            if (users.Count > 1)
            {
                return (false, null, null);
            }
            else if (users.Count == 1)
            {

                var user = users.First();

                var userFullNameToLower = user.FullName.ToLower();

                if (userFullNameToLower == nameLow)
                {
                    var userSection = user.Section.SectionName;

                    var userFullName = user.FullName;

                    return (true, userFullName, userSection);
                }
                else
                {
                    return (false, null, null);
                }
            }
            else
            {
                return (false, null, null);
            }

        }

        public async Task<IList<string>> GetAllRoles()
        {

            IList<IdentityRole> roles = await roleManager.Roles.ToListAsync();

            List<string> roleNames = roles.Select(r => r.Name).ToList();

            return roleNames;
        }

        public async Task<int> CountAllUsers()
        {
            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();
            var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault().ToString();

            if (userRole == "Manager")
            {
                var users = userManager.Users.Where(a => a.SectionId == user.SectionId).Where(a => a.deActivated != true).Count();

                return users;
            }
            else
            {
                var users = userManager.Users.Where(a => a.deActivated != true).Count();

                return users;
            }

        }

        public async Task<bool> DeleteUser(string id)
        {
            if (id != null)
            {

                var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == id);

                if (user != null)
                {

                    user.deActivated = true;

                    await userManager.UpdateAsync(user);

                    appUnitOfWork.Complete();

                    return true;
              

                }
 
                    return false;       
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> AddSection(SectionDTO sectionDTO)
        {

            if (sectionDTO != null)
            {

                Section section = new Section
                {
                    SectionName = sectionDTO.SectionName,
                }; 

                appUnitOfWork.repository<Section>().Add(section);

                await appUnitOfWork.Complete();

                return true;
            }

            return false;
        }


        public async Task<AppUser> GetUserDetails()
        {
            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var user = await userManager.Users.Include(a => a.Section).FirstOrDefaultAsync(a => a.Id == userId);

            return user;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var projects = await appUnitOfWork.repository<Project>().AsQueryable().OrderBy(a => a.Status).ToListAsync();

            return projects;
        }
        public async Task<IEnumerable<Project>> GetActiveProjects()
        {
            var projects = await appUnitOfWork.repository<Project>().AsQueryable().Where(a => a.Status == "ACTIVE").ToListAsync();

            return projects;
        }


        public async Task<IEnumerable<Activity>> GetActivities()
        {
            var activities = await appUnitOfWork.repository<Activity>().AsQueryable().OrderBy(a => a.Status).ToListAsync();

            return activities;
        }

        public async Task<IEnumerable<Activity>> GetActiveActivities()
        {
            var activities = await appUnitOfWork.repository<Activity>().AsQueryable().Where(a => a.Status == "ACTIVE").ToListAsync();

            return activities;
        }

        public async Task<bool> DeleteSection(int id)
        {


            //USER
            var users = await userManager.Users.Where(a => a.SectionId == id).ToListAsync();

            if (users != null)
            {
                foreach (var user in users)
                {
                    user.SectionId = null;
                    await userManager.UpdateAsync(user);

                    await appUnitOfWork.Complete();
                }
            }



            //DATASHEET
            var dataSheets = await appUnitOfWork.repository<DataSheetBus>().AsQueryable().Where(a => a.SectionId == id).ToListAsync();

            if (dataSheets != null)
            {
                foreach (var dataSheet in dataSheets)
                {
                    dataSheet.SectionId = null;
                    appUnitOfWork.repository<DataSheetBus>().Update(dataSheet);

                    await appUnitOfWork.Complete();
                }
            }
 


            //LEGENDD
            var legends = await appUnitOfWork.repository<Legend>().AsQueryable().FirstOrDefaultAsync(a => a.SectionId == id);

            if (legends != null)
            {
                appUnitOfWork.repository<Legend>().Remove(legends);
                await appUnitOfWork.Complete();
            }


            //SECTION
            var section = await appUnitOfWork.repository<Section>().GetFirstOrDefaultAsync(a => a.SectionId == id);

            appUnitOfWork.repository<Section>().Remove(section);

            await appUnitOfWork.Complete();

            return true;
            
        }

        public async Task<bool> UpdateSection(int sectionId, string sectionName)
        {

            if (sectionId != null)
            {

                var section = appUnitOfWork.repository<Section>().AsQueryable().FirstOrDefault(a => a.SectionId == sectionId);

                if (section != null)
                {
                    section.SectionName = sectionName;

                    appUnitOfWork.repository<Section>().Update(section);
                    await appUnitOfWork.Complete();

                    return true;

                }
         

            }


            return false;
        }

        public async Task<bool> DeleteWorkingHours(int id)
        {
            if (id != null)
            {

                var section = await appUnitOfWork.repository<AllowedHours>().GetFirstOrDefaultAsync(a => a.AllowedHoursId == id);

                appUnitOfWork.repository<AllowedHours>().Remove(section);

                await appUnitOfWork.Complete();

                return true;
            }


            return false;
        }

        public async Task<bool> UpdateWorkingHours(int allowedId, int allowedNumber)
        {
            if (allowedId != null)
            {

                var allowed = appUnitOfWork.repository<AllowedHours>().AsQueryable().FirstOrDefault(a => a.AllowedHoursId == allowedId);

                if (allowed != null)
                {
                    allowed.Number = allowedNumber;

                    appUnitOfWork.repository<AllowedHours>().Update(allowed);
                    await appUnitOfWork.Complete();

                    return true;

                }

            }


            return false;
        }



        public async Task<(bool, string)> AddDataSheetUserExist(string name)
        {
            var user = await userManager.Users.Where(a => a.FullName == name).FirstOrDefaultAsync();

            if (user != null)
            {
                var section = appUnitOfWork.repository<Section>().AsQueryable().Where(a => a.SectionId == user.SectionId).FirstOrDefault();

                if (section != null)
                {
                    var userSection = section.SectionName;

                    return (true, userSection);
                }
                else
                {
                    string message = "No";

                    return (false, message);
                }

            }
            else
            {
                string message = "No";

                return (false, message);
            }

        }




        public async Task<bool> AddDataSheet(DataSheetBusDTO dataSheetBusDTO)
        {
            if (dataSheetBusDTO != null)
            {

                var user = userManager.Users.FirstOrDefault(a => a.FullName == dataSheetBusDTO.UserName);

                var project = 0;

                var activity = 0;

                var businessOrIt = 0;


                /////////////Project
          
                var projectName = appUnitOfWork.repository<Project>().AsQueryable().FirstOrDefault(a => a.ProjectName == dataSheetBusDTO.ProjectName);

                if (projectName == null)
                {
                    Project project1 = new Project
                    {
                        ProjectName = dataSheetBusDTO.ProjectName
                    };

              
                    appUnitOfWork.repository<Project>().Add(project1);

                    await appUnitOfWork.Complete();

                    project = project1.ProjectId;
                }
                else
                {
                    project = projectName.ProjectId;
                }

                //////////ACTIVITY


                var activityName = appUnitOfWork.repository<Activity>().AsQueryable().FirstOrDefault(a => a.ActivityName == dataSheetBusDTO.ActivityName);

                if (activityName == null)
                {
                    Activity activity1 = new Activity
                    {
                        ActivityName = dataSheetBusDTO.ActivityName
                    };

             

                    appUnitOfWork.repository<Activity>().Add(activity1);

                    await appUnitOfWork.Complete();

                    activity = activity1.ActivityId;

                }
                else
                {
                    activity = activityName.ActivityId;
                }


                /////////BUSINESS

                var businessOrItName = appUnitOfWork.repository<BusinessOrIt>().AsQueryable().FirstOrDefault(a => a.BusinessOrItName == dataSheetBusDTO.BusinessOrIt);

                if (businessOrItName == null)
                {
                    BusinessOrIt businessOrIt1 = new BusinessOrIt
                    {
                        BusinessOrItName = dataSheetBusDTO.BusinessOrIt
                    };

   

                    appUnitOfWork.repository<BusinessOrIt>().Add(businessOrIt1);

                    await appUnitOfWork.Complete();

                    businessOrIt = businessOrIt1.BusinessOrItId;

                }
                else
                {
                    businessOrIt = businessOrItName.BusinessOrItId;
                }

                ///

               var section = appUnitOfWork.repository<Section>().AsQueryable().FirstOrDefault(a => a.SectionName == dataSheetBusDTO.SectioName);


                DataSheetBus dataSheetBus = new DataSheetBus
                {
                    AppUserId = user.Id,
                    ProjectId = project,
                    ActivityId = activity,
                    BusinessOrItId = businessOrIt,
                    SectionId = section.SectionId,
                    StartDate = dataSheetBusDTO.StartDate,
                    EndDate = dataSheetBusDTO.EndDate,
                    HoursPerDay = dataSheetBusDTO.HoursPerDay,
                    HoursPerMonth = dataSheetBusDTO.HoursPerMonth,
                };

                
                appUnitOfWork.repository<DataSheetBus>().Add(dataSheetBus);

                await appUnitOfWork.Complete();

                return true;

            }

            return false;
        }


        public async Task<bool> UpdateDataSheet(DataSheetBusDTO dataSheetBusDTO)
        {
       
            var dataSheet = await appUnitOfWork.repository<DataSheetBus>().AsQueryable().Where(a => a.DataSheetBusId == dataSheetBusDTO.BusId).FirstOrDefaultAsync();

            var user =  userManager.Users.Where(a => a.FullName == dataSheetBusDTO.UserName).FirstOrDefault(); 

            var project = await appUnitOfWork.repository<Project>().AsQueryable().Where(a => a.ProjectName == dataSheetBusDTO.ProjectName).FirstOrDefaultAsync();

            if(project == null)
            {

                Project project1 = new Project
                {
                    ProjectName = dataSheetBusDTO.ProjectName,
                };

                appUnitOfWork.repository<Project>().Add(project1);
                await appUnitOfWork.Complete();

                dataSheet.ProjectId = project1.ProjectId;
            }
            else
            {
                dataSheet.ProjectId = project.ProjectId;

            }

            var activity = await appUnitOfWork.repository<Activity>().AsQueryable().Where(a => a.ActivityName == dataSheetBusDTO.ActivityName).FirstOrDefaultAsync();

            if (activity == null)
            {

                Activity activity1 = new Activity
                {
                    ActivityName = dataSheetBusDTO.ActivityName,
                };

                appUnitOfWork.repository<Activity>().Add(activity1);
                await appUnitOfWork.Complete();

                dataSheet.ActivityId = activity1.ActivityId;
            }
            else
            {
                dataSheet.ActivityId = activity.ActivityId;
            }


            var businessOrIt = await appUnitOfWork.repository<BusinessOrIt>().AsQueryable().Where(a => a.BusinessOrItName == dataSheetBusDTO.BusinessOrIt).FirstOrDefaultAsync();
            
            var section = await appUnitOfWork.repository<Section>().AsQueryable().Where(a => a.SectionName == dataSheetBusDTO.SectioName).FirstOrDefaultAsync();

            if (dataSheet != null)
            {
                dataSheet.AppUserId = user.Id;
                dataSheet.BusinessOrItId = businessOrIt.BusinessOrItId;
                dataSheet.SectionId = section.SectionId;
                dataSheet.StartDate = dataSheetBusDTO.StartDate;
                dataSheet.EndDate = dataSheetBusDTO.EndDate;
                dataSheet.HoursPerDay = dataSheetBusDTO.HoursPerDay;
                dataSheet.HoursPerMonth = dataSheetBusDTO.HoursPerMonth;

                appUnitOfWork.repository<DataSheetBus>().Update(dataSheet);

                await appUnitOfWork.Complete();

            }

            return true;
        }




        public async Task<IEnumerable<Holidays>> GetHolidays(int year)
        {
            var holidayFixedFinal = appUnitOfWork.repository<Holidays>().AsQueryable().Where(a => a.HolidayStatus.StatusName == "fixed").Include(a => a.HolidayStatus).ToList();
            var movable = appUnitOfWork.repository<Holidays>().AsQueryable().Where(a => a.FixedDate.Year == year && a.HolidayStatus.StatusName == "movable").Include(a => a.HolidayStatus).ToList();

            IEnumerable<Holidays> allHolidays = holidayFixedFinal.OrderBy(h => h.FixedDate.Year).ThenBy(h => h.FixedDate.Month).ThenBy(h => h.FixedDate.Day);

            if (movable != null && movable.Any())
            {
                allHolidays = allHolidays.Concat(movable).OrderBy(h => h.FixedDate.Year).ThenBy(h => h.FixedDate.Month).ThenBy(h => h.FixedDate.Day);
            }

            return allHolidays;
        }


        public async Task<(IEnumerable<Holidays>, string)> GetHolidaysToShow(int year)
        {

            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();
            var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            var holidayFixedFinal = appUnitOfWork.repository<Holidays>().AsQueryable().Where(a => a.HolidayStatus.StatusName == "fixed").Include(a => a.HolidayStatus).ToList();
            var movable = appUnitOfWork.repository<Holidays>().AsQueryable().Where(a => a.FixedDate.Year == year && a.HolidayStatus.StatusName == "movable").Include(a => a.HolidayStatus).ToList();

            IEnumerable<Holidays> allHolidays = holidayFixedFinal.OrderBy(h => h.FixedDate.Year).ThenBy(h => h.FixedDate.Month).ThenBy(h => h.FixedDate.Day);

            if (movable != null && movable.Any())
            {
                allHolidays = allHolidays.Concat(movable).OrderBy(h => h.FixedDate.Year).ThenBy(h => h.FixedDate.Month).ThenBy(h => h.FixedDate.Day);
            }

            return (allHolidays, userRole);
        }


        public async Task<bool> IsHoliday(DateOnly date)
        {
            var holidays = await GetAllFixedHolidays();
            return holidays.Any(h => h.FixedDate == date);
        }

        public bool IsWeekday(DateOnly date)
        {
            return date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday;
        }

        //public async Task<IEnumerable<DataSheetBus>> GetAllDataSheetSort(string? name, int? year)
        //{
        //    var userId = httpcontext.HttpContext.Session.GetString("UsersId")?.ToString();

        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        var userName = name.ToLower();
        //        var users = await userManager.Users
        //            .Where(a => a.FullName.ToLower().Contains(userName))
        //            .Where(a => a.Id != userId)

        //            .ToListAsync();

        //        var data = await appUnitOfWork.repository<DataSheetBus>()
        //            .AsQueryable()
        //            .Where(a => users.Any(u => a.AppUserId == u.Id))
        //            .Where(a => a.EndDate.Year == year || a.StartDate.Year == year)
        //            .Include(a => a.Section)
        //            .Include(a => a.Project)
        //            .Include(a => a.Activity)
        //            .Include(a => a.BusinessOrIt)
        //            .Include(a => a.AppUser)
        //            .OrderBy(a => a.Section)
        //            .ToListAsync();

        //        return data;
        //    }
        //    else
        //    {
        //        var data = await appUnitOfWork.repository<DataSheetBus>()
        //            .AsQueryable()
        //            .Where(a => a.EndDate.Year == year || a.StartDate.Year == year)
        //            .Include(a => a.Section)
        //            .Include(a => a.Project)
        //            .Include(a => a.Activity)
        //            .Include(a => a.BusinessOrIt)
        //            .Include(a => a.AppUser)
        //            .OrderBy(a => a.Section)
        //            .ToListAsync();

        //        return data;
        //    }
        //}



        public async Task<(IEnumerable<DataSheetBus>, string)> GetAllDataSheetSort(int? sectionId, string? name, int? year)
        {

            var userID = httpcontext.HttpContext.Session.GetString("UsersId");

            var user = userManager.Users.FirstOrDefault(a => a.Id == userID);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            string userName = null;

            if (!string.IsNullOrEmpty(name))
            {
                userName = name.ToLower();
            }

          
            Section userSection = null;

            if (userRole == "Project_Manager")
            {

                if (sectionId != null)
                {
                    userSection = appUnitOfWork.repository<Section>().AsQueryable().FirstOrDefault(a => a.SectionId == sectionId);

                    var data = await appUnitOfWork.repository<DataSheetBus>()
                        .AsQueryable()
                        .Where(d => d.StartDate.Year == year || d.EndDate.Year == year)
                        .Where(d => (userSection == null || d.Section.SectionName == userSection.SectionName) && (d.AppUser.FullName.ToLower().Contains(userName) || string.IsNullOrEmpty(name)))
                        .Include(d => d.Section)
                        .Include(d => d.Project)
                        .Include(d => d.Activity)
                        .Include(d => d.BusinessOrIt)
                        .Include(d => d.AppUser)
                        .OrderBy(d => d.Section)
                        .ToListAsync();

                    return (data, userRole);
                }
                else
                {
                    var data = await appUnitOfWork.repository<DataSheetBus>()
                        .AsQueryable()
                        .Where(d => d.StartDate.Year == year || d.EndDate.Year == year)
                        .Where(d => d.AppUser.FullName.ToLower().Contains(userName) || string.IsNullOrEmpty(name))
                        .Include(d => d.Section)
                        .Include(d => d.Project)
                        .Include(d => d.Activity)
                        .Include(d => d.BusinessOrIt)
                        .Include(d => d.AppUser)
                        .OrderBy(d => d.Section)
                        .ToListAsync();

                    return (data, userRole);
                }


            }
            else
            {
                var data = await appUnitOfWork.repository<DataSheetBus>()
                    .AsQueryable()
                    .Include(d => d.Section)
                    .Include(d => d.Project)
                    .Include(d => d.Activity)
                    .Include(d => d.BusinessOrIt)
                    .Include(d => d.AppUser)
                    .OrderBy(d => d.AppUser.FullName)
                    .Where(d => (d.StartDate.Year == year || d.EndDate.Year == year) && d.SectionId == user.SectionId)
                    .Where(d => string.IsNullOrEmpty(userName) ||
                    d.AppUser.FullName.ToLower().Contains(userName) ||
                    d.Project.ProjectName.ToLower().Contains(userName) ||
                    d.Activity.ActivityName.ToLower().Contains(userName) ||
                    d.BusinessOrIt.BusinessOrItName.ToLower().Contains(userName))
                    .ToListAsync();

                return (data, userRole);
            }


        }


        public async Task<IEnumerable<DataSheetBus>> GetAllDataSheet()
        {
            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);
            
            var data = await appUnitOfWork.repository<DataSheetBus>()
                .AsQueryable()
                .Include(a => a.Section)
                .Include(a => a.Project)
                .Include(a => a.Activity)
                .Include(a => a.BusinessOrIt)
                .Include(a => a.AppUser)
                .OrderBy(a => a.Section)
                .ToListAsync();

            return data;
        }

        public async Task<List<UserMonthlyStatistics>> GetUserMonthlyStatistics()
        {
            var datasheet = await GetAllDataSheet();
            var holidays = await GetAllFixedHolidays();


            var userStats = datasheet
                .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
                .Select(g => new UserMonthlyStatistics
                {
                    AppUserId = g.Key.AppUserId,
                    AppUserName = g.FirstOrDefault().AppUser.FullName,
                    SectionId = g.FirstOrDefault().SectionId,
                    SectionName = g.Key.SectionName,
                    Low = 0,
                    Med = 0,
                    Max = 0,
                    January = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 1),
                    February = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 2),
                    March = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 3),
                    April = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 4),
                    May = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 5),
                    June = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 6),
                    July = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 7),
                    August = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 8),
                    September = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 9),
                    October = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 10),
                    November = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 11),
                    December = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 12)
                }).ToList();

            var legends = await appUnitOfWork.repository<Legend>().GetAllAsync();

            foreach (var stat in userStats)
            {
                var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
                if (legend != null)
                {
                    stat.Low = legend.LOW ?? 0;
                    stat.Med = legend.MED ?? 0;
                    stat.Max = legend.MAX ?? 0;
                }

                stat.TotalHours = (stat.January ?? 0) +
                   (stat.February ?? 0) +
                   (stat.March ?? 0) +
                   (stat.April ?? 0) +
                   (stat.May ?? 0) +
                   (stat.June ?? 0) +
                   (stat.July ?? 0) +
                   (stat.August ?? 0) +
                   (stat.September ?? 0) +
                   (stat.October ?? 0) +
                   (stat.November ?? 0) +
                   (stat.December ?? 0);
            }
            return userStats;
        }


        public async Task<List<UserMonthlyStatistics>> GetUserMonthlyStatisticsSort(int? section, string? name, int year)
        {
            var datasheet = await GetAllDataSheet();
            var holidays = await GetHolidays(year);

            var userID = httpcontext.HttpContext.Session.GetString("UsersId");
            var user = userManager.Users.FirstOrDefault(a => a.Id == userID);

            var userRoles = await userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            string userName = null;

            if (!string.IsNullOrEmpty(name))
            {
                userName = name.ToLower();
            }

            Section userSection = null;


            if (userRole == "Project_Manager")
            {

                if (section != null)
                {
                    userSection = appUnitOfWork.repository<Section>().AsQueryable().Where(a => a.SectionId == section).FirstOrDefault();

                    var userStats = datasheet
                   .Where(d => d.StartDate.Year == year || d.EndDate.Year == year)
                   .Where(d => (userName == null || d.AppUser.FullName.ToLower().Contains(userName)) && (userSection == null || d.Section.SectionName == userSection.SectionName))
                   .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
                   .Select(g => new UserMonthlyStatistics
                   {
                       AppUserId = g.Key.AppUserId,
                       AppUserName = g.FirstOrDefault().AppUser.FullName,
                       SectionId = g.FirstOrDefault().SectionId,
                       SectionName = g.Key.SectionName,
                       Low = 0,
                       Med = 0,
                       Max = 0,
                       January = CalculateMonthlyHours(g, holidays, year, 1),
                       February = CalculateMonthlyHours(g, holidays, year, 2),
                       March = CalculateMonthlyHours(g, holidays, year, 3),
                       April = CalculateMonthlyHours(g, holidays, year, 4),
                       May = CalculateMonthlyHours(g, holidays, year, 5),
                       June = CalculateMonthlyHours(g, holidays, year, 6),
                       July = CalculateMonthlyHours(g, holidays, year, 7),
                       August = CalculateMonthlyHours(g, holidays, year, 8),
                       September = CalculateMonthlyHours(g, holidays, year, 9),
                       October = CalculateMonthlyHours(g, holidays, year, 10),
                       November = CalculateMonthlyHours(g, holidays, year, 11),
                       December = CalculateMonthlyHours(g, holidays, year, 12)
                   }).ToList();

                    var legends = await appUnitOfWork.repository<Legend>().GetAllAsync();

                    foreach (var stat in userStats)
                    {
                        var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
                        if (legend != null)
                        {
                            stat.Low = legend.LOW ?? 0;
                            stat.Med = legend.MED ?? 0;
                            stat.Max = legend.MAX ?? 0;
                        }

                        stat.TotalHours = (stat.January ?? 0) +
                           (stat.February ?? 0) +
                           (stat.March ?? 0) +
                           (stat.April ?? 0) +
                           (stat.May ?? 0) +
                           (stat.June ?? 0) +
                           (stat.July ?? 0) +
                           (stat.August ?? 0) +
                           (stat.September ?? 0) +
                           (stat.October ?? 0) +
                           (stat.November ?? 0) +
                           (stat.December ?? 0);
                    }
                    return userStats;
                }
                else
                {

                    var userStats = datasheet
                    .Where(d => d.StartDate.Year == year || d.EndDate.Year == year)
                    .Where(d => userName == null || d.AppUser.FullName.ToLower().Contains(userName))
                    .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
                    .Select(g => new UserMonthlyStatistics
                    {
                       AppUserId = g.Key.AppUserId,
                       AppUserName = g.FirstOrDefault().AppUser.FullName,
                       SectionId = g.FirstOrDefault().SectionId,
                       SectionName = g.Key.SectionName,
                       Low = 0,
                       Med = 0,
                       Max = 0,
                       January = CalculateMonthlyHours(g, holidays, year, 1),
                       February = CalculateMonthlyHours(g, holidays, year, 2),
                       March = CalculateMonthlyHours(g, holidays, year, 3),
                       April = CalculateMonthlyHours(g, holidays, year, 4),
                       May = CalculateMonthlyHours(g, holidays, year, 5),
                       June = CalculateMonthlyHours(g, holidays, year, 6),
                       July = CalculateMonthlyHours(g, holidays, year, 7),
                       August = CalculateMonthlyHours(g, holidays, year, 8),
                       September = CalculateMonthlyHours(g, holidays, year, 9),
                       October = CalculateMonthlyHours(g, holidays, year, 10),
                       November = CalculateMonthlyHours(g, holidays, year, 11),
                       December = CalculateMonthlyHours(g, holidays, year, 12)
                    }).ToList();

                    var legends = await appUnitOfWork.repository<Legend>().GetAllAsync();

                    foreach (var stat in userStats)
                    {
                        var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
                        if (legend != null)
                        {
                            stat.Low = legend.LOW ?? 0;
                            stat.Med = legend.MED ?? 0;
                            stat.Max = legend.MAX ?? 0;
                        }

                        stat.TotalHours = (stat.January ?? 0) +
                           (stat.February ?? 0) +
                           (stat.March ?? 0) +
                           (stat.April ?? 0) +
                           (stat.May ?? 0) +
                           (stat.June ?? 0) +
                           (stat.July ?? 0) +
                           (stat.August ?? 0) +
                           (stat.September ?? 0) +
                           (stat.October ?? 0) +
                           (stat.November ?? 0) +
                           (stat.December ?? 0);
                    }
                    return userStats;

                }


            }
            else
            {
                var userStats = datasheet
                .Where(d => d.StartDate.Year == year || d.EndDate.Year == year)
                .Where(d => (userName == null || d.AppUser.FullName.ToLower().Contains(userName)) && (d.Section.SectionName == user.Section.SectionName))
                .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
                .Select(g => new UserMonthlyStatistics
                {
                   AppUserId = g.Key.AppUserId,
                   AppUserName = g.FirstOrDefault().AppUser.FullName,
                   SectionId = g.FirstOrDefault().SectionId,
                   SectionName = g.Key.SectionName,
                   Low = 0,
                   Med = 0,
                   Max = 0,
                   January = CalculateMonthlyHours(g, holidays, year, 1),
                   February = CalculateMonthlyHours(g, holidays, year, 2),
                   March = CalculateMonthlyHours(g, holidays, year, 3),
                   April = CalculateMonthlyHours(g, holidays, year, 4),
                   May = CalculateMonthlyHours(g, holidays, year, 5),
                   June = CalculateMonthlyHours(g, holidays, year, 6),
                   July = CalculateMonthlyHours(g, holidays, year, 7),
                   August = CalculateMonthlyHours(g, holidays, year, 8),
                   September = CalculateMonthlyHours(g, holidays, year, 9),
                   October = CalculateMonthlyHours(g, holidays, year, 10),
                   November = CalculateMonthlyHours(g, holidays, year, 11),
                   December = CalculateMonthlyHours(g, holidays, year, 12)
                }).ToList();

                var legends = await appUnitOfWork.repository<Legend>().GetAllAsync();

                foreach (var stat in userStats)
                {
                    var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
                    if (legend != null)
                    {
                        stat.Low = legend.LOW ?? 0;
                        stat.Med = legend.MED ?? 0;
                        stat.Max = legend.MAX ?? 0;
                    }

                    stat.TotalHours = (stat.January ?? 0) +
                       (stat.February ?? 0) +
                       (stat.March ?? 0) +
                       (stat.April ?? 0) +
                       (stat.May ?? 0) +
                       (stat.June ?? 0) +
                       (stat.July ?? 0) +
                       (stat.August ?? 0) +
                       (stat.September ?? 0) +
                       (stat.October ?? 0) +
                       (stat.November ?? 0) +
                       (stat.December ?? 0);
                }
                return userStats;
            }

        }

         

        public async Task<List<UserMonthlyStatistics>> GetUserMonthlyStatisticsEmployeeSort(int year)
        {
            var datasheet = await GetAllDataSheet();
            var holidays = await GetHolidays(year);

            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var userStats = datasheet.Where(a => a.AppUserId == userId)
                .Where(d => d.StartDate.Year == year || d.EndDate.Year == year)
                .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
                .Select(g => new UserMonthlyStatistics
                {
                    AppUserId = g.Key.AppUserId,
                    AppUserName = g.FirstOrDefault().AppUser.FullName,
                    SectionId = g.FirstOrDefault().SectionId,
                    SectionName = g.Key.SectionName,
                    Low = 0,
                    Med = 0,
                    Max = 0,
                    January = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 1),
                    February = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 2),
                    March = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 3),
                    April = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 4),
                    May = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 5),
                    June = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 6),
                    July = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 7),
                    August = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 8),
                    September = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 9),
                    October = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 10),
                    November = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 11),
                    December = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 12)
                }).ToList();

            var legends = await appUnitOfWork.repository<Legend>().GetAllAsync();

            foreach (var stat in userStats)
            {
                var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
                if (legend != null)
                {
                    stat.Low = legend.LOW ?? 0;
                    stat.Med = legend.MED ?? 0;
                    stat.Max = legend.MAX ?? 0;
                }

                stat.TotalHours = (stat.January ?? 0) +
                   (stat.February ?? 0) +
                   (stat.March ?? 0) +
                   (stat.April ?? 0) +
                   (stat.May ?? 0) +
                   (stat.June ?? 0) +
                   (stat.July ?? 0) +
                   (stat.August ?? 0) +
                   (stat.September ?? 0) +
                   (stat.October ?? 0) +
                   (stat.November ?? 0) +
                   (stat.December ?? 0);
            }
            return userStats;
        }



        public async Task<List<UserMonthlyStatistics>> GetUserMonthlyStatisticsEmployee()
        {
            var datasheet = await GetAllDataSheet();
            var holidays = await GetAllFixedHolidays();

            var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var userStats = datasheet.Where(a => a.AppUserId == userId)
                .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
                .Select(g => new UserMonthlyStatistics
                {
                    AppUserId = g.Key.AppUserId,
                    AppUserName = g.FirstOrDefault().AppUser.FullName,
                    SectionId = g.FirstOrDefault().SectionId,
                    SectionName = g.Key.SectionName,
                    Low = 0,
                    Med = 0,
                    Max = 0,
                    January = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 1),
                    February = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 2),
                    March = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 3),
                    April = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 4),
                    May = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 5),
                    June = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 6),
                    July = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 7),
                    August = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 8),
                    September = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 9),
                    October = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 10),
                    November = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 11),
                    December = CalculateMonthlyHours(g, holidays, DateTime.Now.Year, 12)
                }).ToList();

            var legends = await appUnitOfWork.repository<Legend>().GetAllAsync();

            foreach (var stat in userStats)
            {
                var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
                if (legend != null)
                {
                    stat.Low = legend.LOW ?? 0;
                    stat.Med = legend.MED ?? 0;
                    stat.Max = legend.MAX ?? 0;
                }

                stat.TotalHours = (stat.January ?? 0) +
                   (stat.February ?? 0) +
                   (stat.March ?? 0) +
                   (stat.April ?? 0) +
                   (stat.May ?? 0) +
                   (stat.June ?? 0) +
                   (stat.July ?? 0) +
                   (stat.August ?? 0) +
                   (stat.September ?? 0) +
                   (stat.October ?? 0) +
                   (stat.November ?? 0) +
                   (stat.December ?? 0);

            }

            return userStats;
        }

        public async Task<IEnumerable<Holidays>> GetAllFixedHolidays()
        {

            var dateNow = DateOnly.FromDateTime(DateTime.Today);
            var currentYear = dateNow.Year;

            var holidayFixedFinal = appUnitOfWork.repository<Holidays>().AsQueryable().Where(a => a.HolidayStatus.StatusName == "fixed").Include(a => a.HolidayStatus).ToList();

            foreach (var holiday in holidayFixedFinal)
            {
                if (holiday.FixedDate.Year != currentYear)
                {
                    holiday.FixedDate = new DateOnly(currentYear, holiday.FixedDate.Month, holiday.FixedDate.Day);
                    appUnitOfWork.repository<Holidays>().Update(holiday);

                    await appUnitOfWork.Complete();
                }
            }

            var movable = appUnitOfWork.repository<Holidays>().AsQueryable().Where(a => a.FixedDate.Year == currentYear && a.HolidayStatus.StatusName == "movable").Include(a => a.HolidayStatus).ToList();

            IEnumerable<Holidays> allHolidays = holidayFixedFinal.OrderBy(h => h.FixedDate.Year).ThenBy(h => h.FixedDate.Month).ThenBy(h => h.FixedDate.Day);

            if (movable != null && movable.Any())
            {
                allHolidays = allHolidays.Concat(movable).OrderBy(h => h.FixedDate.Year).ThenBy(h => h.FixedDate.Month).ThenBy(h => h.FixedDate.Day);
            }

            return allHolidays;
        }






        private double CalculateMonthlyHours(IEnumerable<DataSheetBus> records, IEnumerable<Holidays> holidays, int year, int month)
        {
            double totalMonthlyHours = 0;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            foreach (var record in records)
            {
                var monthStartDate = new DateOnly(year, month, 1);
                var monthEndDate = new DateOnly(year, month, daysInMonth);

                if (record.StartDate <= monthEndDate && record.EndDate >= monthStartDate)
                {
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        var currentDate = new DateOnly(year, month, day);
                        if (record.StartDate <= currentDate && record.EndDate >= currentDate && IsWeekday(currentDate) && !holidays.Any(h => h.FixedDate.Month == currentDate.Month && h.FixedDate.Day == currentDate.Day))
                        {
                            totalMonthlyHours += record.HoursPerDay;
                        }
                    }
                }
            }

            return totalMonthlyHours;
        }


        //public async Task<List<UserMonthlyStatistics>> GetUserMonthlyStatistics()
        //{
        //    var currentYear = DateTime.Now.Year;

        //    var statistics = await appUnitOfWork.repository<DataSheetBus>().AsQueryable()
        //        .Where(d => d.StartDate.Year == currentYear)
        //        .GroupBy(d => new { d.AppUserId, d.Section.SectionName })
        //        .Select(g => new UserMonthlyStatistics
        //        {
        //            AppUserId = g.Key.AppUserId,
        //            AppUserName = g.FirstOrDefault().AppUser.FullName,
        //            SectionId = g.FirstOrDefault().SectionId,
        //            SectionName = g.Key.SectionName,
        //            January = (int)Math.Round(g.Where(d => d.StartDate.Month == 1 && d.EndDate.Month == 1).Sum(d => d.HoursPerMonth)),
        //            February = (int)Math.Round(g.Where(d => d.StartDate.Month == 2 && d.EndDate.Month == 2).Sum(d => d.HoursPerMonth)),
        //            March = (int)Math.Round(g.Where(d => d.StartDate.Month == 3 && d.EndDate.Month == 3).Sum(d => d.HoursPerMonth)),
        //            April = (int)Math.Round(g.Where(d => d.StartDate.Month == 4 && d.EndDate.Month == 4).Sum(d => d.HoursPerMonth)),
        //            May = (int)Math.Round(g.Where(d => d.StartDate.Month == 5 && d.EndDate.Month == 5).Sum(d => d.HoursPerMonth)),
        //            June = (int)Math.Round(g.Where(d => d.StartDate.Month == 6 && d.EndDate.Month == 6).Sum(d => d.HoursPerMonth)),
        //            July = (int)Math.Round(g.Where(d => d.StartDate.Month == 7 && d.EndDate.Month == 7).Sum(d => d.HoursPerMonth)),
        //            August = (int)Math.Round(g.Where(d => d.StartDate.Month == 8 && d.EndDate.Month == 8).Sum(d => d.HoursPerMonth)),
        //            September = (int)Math.Round(g.Where(d => d.StartDate.Month == 9 && d.EndDate.Month == 9).Sum(d => d.HoursPerMonth)),
        //            October = (int)Math.Round(g.Where(d => d.StartDate.Month == 10 && d.EndDate.Month == 10).Sum(d => d.HoursPerMonth)),
        //            November = (int)Math.Round(g.Where(d => d.StartDate.Month == 11 && d.EndDate.Month == 11).Sum(d => d.HoursPerMonth)),
        //            December = (int)Math.Round(g.Where(d => d.StartDate.Month == 12 && d.EndDate.Month == 12).Sum(d => d.HoursPerMonth)),
        //            TotalHours = (int)Math.Round(g.Sum(d => d.HoursPerMonth)),
        //            Low = 0,
        //            Med = 0,
        //            Max = 0
        //        })
        //        .ToListAsync();


        //    var legends = await appUnitOfWork.repository<Legend>().AsQueryable().ToListAsync();


        //    foreach (var stat in statistics)
        //    {
        //        var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
        //        if (legend != null)
        //        {
        //            stat.Low = legend.LOW ?? 0;
        //            stat.Med = legend.MED ?? 0;
        //            stat.Max = legend.MAX ?? 0;
        //        }
        //    }

        //    return statistics;
        //}



        //public async Task<IEnumerable<FixedHoliday>> GetAllFixedHolidays()
        //{
        //    var fixedHolidays = await appUnitOfWork.repository<FixedHoliday>().AsQueryable().ToListAsync();
        //    return fixedHolidays;
        //}

        //public async Task<bool> IsHoliday(DateOnly date)
        //{
        //    var holidays = await GetAllFixedHolidays();
        //    return holidays.Any(a => a.FixedDate == date);
        //}

        //public bool IsWeekday(DateOnly date)
        //{
        //    return date.DayOfWeek > DayOfWeek.Sunday && date.DayOfWeek < DayOfWeek.Saturday;
        //}

        //public async Task<IEnumerable<DataSheetBus>> GetAllDataSheet()
        //{
        //    var data = await appUnitOfWork.repository<DataSheetBus>()
        //        .AsQueryable()
        //        .Include(a => a.Section)
        //        .Include(a => a.Project)
        //        .Include(a => a.Activity)
        //        .Include(a => a.BusinessOrIt)
        //        .Include(a => a.AppUser)
        //        .OrderBy(a => a.Section)
        //        .ToListAsync();

        //    return data;
        //}

        //public async Task<List<UserMonthlyStatistics>> GetUserMonthlyStatistics()
        //{
        //    var datasheet = await GetAllDataSheet();
        //    var holidays = await GetAllFixedHolidays();

        //    var userStats = new List<UserMonthlyStatistics>();

        //    // Group the data by user and section
        //    var groupedData = datasheet.GroupBy(d => new { d.AppUserId, d.Section.SectionName });

        //    // Iterate through the grouped data and calculate the monthly statistics
        //    foreach (var group in groupedData)
        //    {
        //        var userStat = new UserMonthlyStatistics
        //        {
        //            AppUserId = group.Key.AppUserId,
        //            AppUserName = group.FirstOrDefault().AppUser.FullName,
        //            SectionId = group.FirstOrDefault().SectionId,
        //            SectionName = group.Key.SectionName,
        //            Low = 0,
        //            Med = 0,
        //            Max = 0
        //        };

        //        // Calculate the monthly hours for each month
        //        for (int month = 1; month <= 12; month++)
        //        {
        //            string monthPropertyName = GetMonthPropertyName(month);
        //            userStat.GetType().GetProperty(monthPropertyName).SetValue(userStat, CalculateMonthlyHours(group, holidays, DateTime.Now.Year, month));
        //        }

        //        userStats.Add(userStat);
        //    }

        //    // Fetch the legend data and update the user stats
        //    var legends = await appUnitOfWork.repository<Legend>().AsQueryable().ToListAsync();
        //    foreach (var stat in userStats)
        //    {
        //        var legend = legends.FirstOrDefault(l => l.SectionId == stat.SectionId);
        //        if (legend != null)
        //        {
        //            stat.Low = legend.LOW ?? 0;
        //            stat.Med = legend.MED ?? 0;
        //            stat.Max = legend.MAX ?? 0;
        //        }
        //    }

        //    return userStats;
        //}

        //private double CalculateMonthlyHours(IEnumerable<DataSheetBus> records, IEnumerable<FixedHoliday> holidays, int year, int month)
        //{
        //    double totalMonthlyHours = 0;

        //    foreach (var record in records)
        //    {
        //        var monthStartDate = new DateOnly(year, month, 1);
        //        var monthEndDate = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

        //        var recordStartDate = record.StartDate;
        //        var recordEndDate = record.EndDate;

        //        var recordMonthStartDate = DateOnly.MaxValue;
        //        var recordMonthEndDate = DateOnly.MinValue;

        //        if (recordStartDate <= monthEndDate && recordEndDate >= monthStartDate)
        //        {
        //            recordMonthStartDate = recordStartDate > monthStartDate ? recordStartDate : monthStartDate;
        //            recordMonthEndDate = recordEndDate < monthEndDate ? recordEndDate : monthEndDate;

        //            var daysInMonth = (double)(recordMonthEndDate.ToDateTime(TimeOnly.MinValue) -
        //                                      recordMonthStartDate.ToDateTime(TimeOnly.MinValue)).TotalDays + 1;

        //            var isHoliday = holidays.Any(h => h.FixedDate >= recordMonthStartDate && h.FixedDate <= recordMonthEndDate);
        //            var isWeekday = IsWeekday(recordMonthStartDate);

        //            if (isWeekday && !isHoliday)
        //            {
        //                var monthlyHours = daysInMonth * record.HoursPerDay;
        //                totalMonthlyHours += monthlyHours;
        //            }
        //        }
        //    }

        //    return totalMonthlyHours;
        //}

        //private string GetMonthPropertyName(int month)
        //{
        //    switch (month)
        //    {
        //        case 1: return nameof(UserMonthlyStatistics.January);
        //        case 2: return nameof(UserMonthlyStatistics.February);
        //        case 3: return nameof(UserMonthlyStatistics.March);
        //        case 4: return nameof(UserMonthlyStatistics.April);
        //        case 5: return nameof(UserMonthlyStatistics.May);
        //        case 6: return nameof(UserMonthlyStatistics.June);
        //        case 7: return nameof(UserMonthlyStatistics.July);
        //        case 8: return nameof(UserMonthlyStatistics.August);
        //        case 9: return nameof(UserMonthlyStatistics.September);
        //        case 10: return nameof(UserMonthlyStatistics.October);
        //        case 11: return nameof(UserMonthlyStatistics.November);
        //        case 12: return nameof(UserMonthlyStatistics.December);
        //        default: return string.Empty;
        //    }
        //}



        public async Task<LegendDTO> Legend(int? sectionId)
        {

            if (sectionId == null)
            {

                var userId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

                var user = await userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

                var userRoles = await userManager.GetRolesAsync(user);

                var userRole = userRoles.FirstOrDefault();


                if (userRole == "Manager")
                {
                    var legend = await appUnitOfWork.repository<Legend>().AsQueryable()
                        .Where(a => a.SectionId == user.SectionId)
                        .FirstOrDefaultAsync();

                    if (legend == null)
                        return null;

                    return new LegendDTO
                    {
                        MAX = legend.MAX,
                        MED = legend.MED,
                        LOW = legend.LOW
                    };
                }
                else
                {
                    return null;
                }

            }
            else {

                    var legend = await appUnitOfWork.repository<Legend>().AsQueryable()
                        .Where(a => a.SectionId == sectionId)
                        .FirstOrDefaultAsync();

                    if (legend == null)
                        return null;

                    return new LegendDTO
                    {
                        MAX = legend.MAX,
                        MED = legend.MED,
                        LOW = legend.LOW
                    };
 
            }  
        }


        public async Task<bool> DeleteDataSheet(int dataSheetId)
        {

            var datasheet = await appUnitOfWork.repository<DataSheetBus>().AsQueryable().Where(a => a.DataSheetBusId == dataSheetId).FirstOrDefaultAsync();

            appUnitOfWork.repository<DataSheetBus>().Remove(datasheet);

            await appUnitOfWork.Complete();

            return true;
        }

        public async Task<bool> AddLegend(AddLegendDTO addLegendDTO)
        {

            if (addLegendDTO != null)
            {
                Legend legend = new Legend
                {
                    SectionId = addLegendDTO.sectionId,
                    LOW = addLegendDTO.LOW,
                    MED = addLegendDTO.MED,
                    MAX = addLegendDTO.MAX
                };

                appUnitOfWork.repository<Legend>().Add(legend);
                await appUnitOfWork.Complete();

                return true;
            }
            else
            {
                return false;
            }

        }



        public async Task<IEnumerable<DataSheetBus>> GetAllDataSheetEmpolyee()
        {
            var currentUser = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var data = await appUnitOfWork.repository<DataSheetBus>()
            .AsQueryable()
            .Where(a => a.AppUserId == currentUser)
            .Include(a => a.Section)
            .Include(a => a.Project)
            .Include(a => a.Activity)
            .Include(a => a.BusinessOrIt)
            .Include(a => a.AppUser)
            .OrderBy(a => a.AppUser.FullName)
            .OrderBy(d => d.StartDate)
            .ToListAsync();

            return data;
        }

        public Task<bool> CheckNumberIfExist(int number)
        {
            throw new NotImplementedException();
        }




        public async Task<bool> AddWorkingHours(AllowedHours allowedHours)
        {

            appUnitOfWork.repository<AllowedHours>().Add(allowedHours);

            await appUnitOfWork.Complete();

            return true;
        }

        public async Task<IEnumerable<AllowedHours>> GetAllowedHours()
        {

            var allowedHours =  await appUnitOfWork.repository<AllowedHours>().AsQueryable().ToListAsync();


            return allowedHours;
        }

        public async Task<bool> AddMovable(MovableHolidaysDTO model)
        {
            if (model.movableNameForAdditional != null)
            {

                Holidays holiday = new Holidays
                {
                    FixedName = model.movableNameForAdditional,
                    FixedDate = (DateOnly)model.movableDate,
                    HolidayStatusId = 2
                };


                appUnitOfWork.repository<Holidays>().Add(holiday);

                await appUnitOfWork.Complete(); 

                return true;
            }
            else
            {
                Holidays holiday = new Holidays
                {
                    FixedName = model.movableName,
                    FixedDate = (DateOnly)model.movableDate,
                    HolidayStatusId = 2
                };


                appUnitOfWork.repository<Holidays>().Add(holiday);

                await appUnitOfWork.Complete();

                return true;
            }

        }



        public async Task<bool> UpdateMovable(MovableHolidaysDTO model)
        {

            if (model.movableNameForAdditional != null)
            {

                var holiday = appUnitOfWork.repository<Holidays>().AsQueryable().FirstOrDefault(h => h.FixedId == model.Id);

                holiday.FixedName = model.movableNameForAdditional;
                holiday.FixedDate = (DateOnly)model.movableDate;

                appUnitOfWork.repository<Holidays>().Update(holiday);

                await appUnitOfWork.Complete();

                return true;
            }
            else
            {
                var holiday = appUnitOfWork.repository<Holidays>().AsQueryable().FirstOrDefault(h => h.FixedId == model.Id);

                holiday.FixedName = model.movableName;
                holiday.FixedDate = (DateOnly)model.movableDate;

                appUnitOfWork.repository<Holidays>().Update(holiday);

                await appUnitOfWork.Complete();

                return true;
            }
        }



        public async Task<bool> UpdateProject(int id, string projectName, string status)
        {

            var project = await appUnitOfWork.repository<Project>().AsQueryable().FirstOrDefaultAsync(h => h.ProjectId == id);

            project.ProjectName = projectName;
            project.Status = status;    

            appUnitOfWork.repository<Project>().Update(project);

            await appUnitOfWork.Complete();

            return true;

        }


        public async Task<bool> UpdateActivity(int id, string activityName, string status)
        {

            var activity = await appUnitOfWork.repository<Activity>().AsQueryable().FirstOrDefaultAsync(h => h.ActivityId == id);

            activity.ActivityName = activityName;
            activity.Status = status;


            appUnitOfWork.repository<Activity>().Update(activity);

            await appUnitOfWork.Complete();

            return true;

        }


        public async Task<IEnumerable<Legend>> GetLegends()
        {
           
            var legends = await appUnitOfWork.repository<Legend>().AsQueryable().Include(a => a.Section).ToListAsync();

            return legends;

        }

        public async Task<bool> UpdateLegend(AddLegendDTO addLegendDTO)
        {

            var legend = await appUnitOfWork.repository<Legend>().AsQueryable().FirstOrDefaultAsync(a => a.LegendId == addLegendDTO.legendID);

            legend.SectionId = addLegendDTO.sectionId;
            legend.LOW = addLegendDTO.LOW;
            legend.MED = addLegendDTO.MED;
            legend.MAX = addLegendDTO.MAX;

             appUnitOfWork.repository<Legend>().Update(legend);
             
             await appUnitOfWork.Complete(); 

            return true;
        }

        public async Task<bool> DeleteLegend(int id)
        {
            var legend = await appUnitOfWork.repository<Legend>().AsQueryable().FirstOrDefaultAsync(a => a.LegendId == id);

            appUnitOfWork.repository<Legend>().Remove(legend);
             
            await appUnitOfWork.Complete();

            return true;

        }

        public async Task<IEnumerable<MovableNames>> GetMovableNames()
        {
          
            var names = await appUnitOfWork.repository<MovableNames>().GetAllAsync();

            return names;

        }


        public async Task<bool> DeleteMovable(int id)
        {

            var holiday = await appUnitOfWork.repository<Holidays>().AsQueryable().FirstOrDefaultAsync(a => a.FixedId == id);

            appUnitOfWork.repository<Holidays>().Remove(holiday);

            await appUnitOfWork.Complete();

            return true;
        }

        public async Task<IEnumerable<BusinessOrIt>> GetBusinessOrIt()
        {
            
            var business = await appUnitOfWork.repository<BusinessOrIt>().GetAllAsync();


            return business;

        }
    }
}

using EmployeeProject.Application.DTOs.Admin;
using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.Interfaces
{
    public interface IAdminServices
    {
        Task<bool> AddProject(string name);
        Task<bool> AddActivity(string name);

        Task<bool> UpdateActivity(int id, string activityName, string status);

        Task<bool> UpdateProfile(IFormFile image);

        Task<bool> UpdateProject(int id, string projectName, string status);
        Task<IEnumerable<AppUser>> GetAllUsers();

        Task<int> CountAllUsers(); 
        Task<IEnumerable<Activity>> GetActivities();
        Task<IEnumerable<Activity>> GetActiveActivities();
        Task<IEnumerable<Project>> GetProjects();
        Task<IEnumerable<Project>> GetActiveProjects();

        Task<IEnumerable<Section>> GetSections();

        Task<List<List<SectionForAddLegendDTO>>> GetLegendSections();
        Task<IEnumerable<Legend>> GetLegends();

        Task<IEnumerable<BusinessOrIt>> GetBusinessOrIt();

        Task<IEnumerable<AllowedHours>> GetAllowedHours();
        Task<AppUser> GetUserDetails();
        Task<IEnumerable<AppUser>> GetUserAddDataSheet(string name);

        Task<IEnumerable<AppUser>> GetUserSearch(string? name);

        Task<(bool, string, string)> AddDataSheetUserExistEnableButton(string name);
        Task<bool> DeleteUser(string id);

        Task<bool> DeleteSection(int id);

        Task<bool> DeleteLegend(int id);

        Task<bool> UpdateSection(int sectionId, string sectionName);


        Task<bool> DeleteWorkingHours(int id);

        Task<bool> UpdateWorkingHours(int allowedId, int allowedNumber);


        Task<bool> AddSection(SectionDTO section);
        Task<(IEnumerable<Holidays>, string)> GetHolidaysToShow(int year);

        Task<List<UserMonthlyStatistics>> GetUserMonthlyStatisticsEmployeeSort(int year);

        //Task<bool> AddMovable(MovableHolidaysDTO model);
        Task<bool> AddMovable(MovableHolidaysDTO model);

        Task<bool> DeleteMovable(int model);

        Task<(bool, string)> AddDataSheetUserExist(string name);

        Task<bool> AddDataSheet(DataSheetBusDTO dataSheetBusDTO);

        Task<bool> AddWorkingHours(AllowedHours allowedHours);
        

        Task<bool> UpdateDataSheet(DataSheetBusDTO dataSheetBusDTO);

        Task<IEnumerable<DataSheetBus>> GetAllDataSheet();

        Task<(IEnumerable<DataSheetBus>, string)> GetAllDataSheetSort(int? sectionId, string? name, int? year);

        Task<IEnumerable<Holidays>> GetAllFixedHolidays();

        Task<IEnumerable<Holidays>> GetHolidays(int year);

        Task<bool> UpdateMovable(MovableHolidaysDTO model);

        Task<List<UserMonthlyStatistics>> GetUserMonthlyStatistics();

        Task<List<UserMonthlyStatistics>> GetUserMonthlyStatisticsSort(int? section, string? name, int year);

        Task<LegendDTO> Legend(int? sectionId);

        Task<bool> DeleteDataSheet(int dataSheetId);

        Task<bool> AddLegend(AddLegendDTO addLegendDTO);


        Task<bool> UpdateLegend(AddLegendDTO addLegendDTO);

        Task<IEnumerable<AppUser>> GetUserAddNewDataForSectionOnly(string name);


        Task<IEnumerable<DataSheetBus>> GetAllDataSheetEmpolyee();
        Task<List<UserMonthlyStatistics>> GetUserMonthlyStatisticsEmployee();

        Task<bool> CheckNumberIfExist(int number);


        Task<bool> IsHoliday(DateOnly date);

         bool IsWeekday(DateOnly date);

        Task<IEnumerable<MovableNames>> GetMovableNames();

        Task<IList<string>> GetAllRoles();
    }
}

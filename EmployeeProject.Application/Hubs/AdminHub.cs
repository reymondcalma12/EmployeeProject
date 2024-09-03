using EmployeeProject.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeProject.Application.Hubs
{
    public class AdminHub : Hub
    {
        private readonly IAdminServices adminServices;

        private readonly IEmployeeServices employeeServices;

        public AdminHub(IAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }

        public async Task GetAllUsers()
        {
            try
            {
                var allUsers = await adminServices.GetAllUsers();

                await Clients.All.SendAsync("ReceiveAllUsers", allUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllUsers: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching users.");
            }
        }

        public async Task GetAllRoles()
        {
            try
            {
                var allRoles = await adminServices.GetAllRoles();

                await Clients.All.SendAsync("ReceiveAllRoles", allRoles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllUsers: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching users.");
            }
        }

        public async Task GetUserSearch(string? name, int? section)
        {
            try
            {
                var allUsersSearch = await adminServices.GetUserSearch(name, section);

                await Clients.Caller.SendAsync("ReceiveAllUsersSearch", allUsersSearch);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllUsers: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching users.");
            }
        }

        public async Task GetUserAddDataSheet(string name)
        {
            try
            {
                var allUsersSearch = await adminServices.GetUserAddDataSheet(name);

                await Clients.Caller.SendAsync("GetUserAddDataSheetResult", allUsersSearch);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllUsers: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching users.");
            }
        }

        public async Task GetUserAddNewDataForSectionOnly(string name)
        {
            try
            {
                var allUsersSearch = await adminServices.GetUserAddNewDataForSectionOnly(name);

                await Clients.Caller.SendAsync("GetUserAddNewDataForSectionOnlyResult", allUsersSearch);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllUsers: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching users.");
            }
        }

        public async Task GetSections()
        {
            try
            {

                var sections = await adminServices.GetSections();
                
                 await Clients.Caller.SendAsync("SectionsDropdown", sections);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetSections: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching sections.");
            }

        }


        public async Task GetLegendSections()
        {
            try
            {
                var sections = await adminServices.GetLegendSections();
                await Clients.Caller.SendAsync("LegendSectionsDropdown", sections);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetLegendSectionsAsync: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching sections and legends.");
            }
        }


        public async Task GetAllowedHours()
        {
            try
            {

                var allowed = await adminServices.GetAllowedHours();

                await Clients.Caller.SendAsync("AllowedHours", allowed);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetSections: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching sections.");
            }

        }



        public async Task GetAllUsersCount()
        {

            try
            {

                var users = await adminServices.CountAllUsers();

                await Clients.All.SendAsync("AllUsersCount", users);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllUsersCount: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllUsersCount.");
            }

        }


        public async Task GetAllDataSheet()
        {

            try
            {

                var data = await adminServices.GetAllDataSheet();

                await Clients.All.SendAsync("AllDataSheet", data);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllDataSheet: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllUsersCount.");
            }

        }

        //   HERE
        public async Task GetAllDataSheetSort(int? sectionId, string? name, int? year)
        {

            try
            {

                var (data, success) = await adminServices.GetAllDataSheetSort(sectionId, name, year);

                await Clients.Caller.SendAsync("GetAllDataSheetSort", new { Data = data, Success = success });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllDataSheet: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllUsersCount.");
            }

        }



        public async Task GetAllDataSheetEmployee()
        {

            try
            {
                var data = await adminServices.GetAllDataSheetEmpolyee();

                await Clients.All.SendAsync("AllDataSheetEmployee", data);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllDataSheet: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllUsersCount.");
            }

        }


        public async Task GetMovableNames()
        {

            try
            {

                var names = await adminServices.GetMovableNames();

                await Clients.All.SendAsync("MovableNames", names);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllHolidays.");
            }


        }




        public async Task GetAllHolidays()
        {

            try
            {

                var holi = await adminServices.GetAllFixedHolidays();

                var holidays = holi.OrderBy(a => a.FixedDate);

                await Clients.Caller.SendAsync("Holidays", holidays);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllHolidays.");
            }


        }



        public async Task GetHolidays(int year)
        {

            try
            {

                var holi = await adminServices.GetHolidays(year);

                var holidays = holi.OrderBy(a => a.FixedDate);

                await Clients.Caller.SendAsync("HolidaysByYear", holidays);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetHolidays.");
            }
        }



        public async Task GetUsersMonthlyStatistics()
        {
            try
            {

                var usersMonthlyStatistics = await adminServices.GetUserMonthlyStatistics();

                await Clients.All.SendAsync("UsersMonthlyStatistics", usersMonthlyStatistics);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllMovableHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllMovableHolidays.");
            }

        }


        public async Task GetUsersMonthlyStatisticsSort(int? sectionId, string? name, int year)
        {
            try
            {

                var usersMonthlyStatisticsSort = await adminServices.GetUserMonthlyStatisticsSort(sectionId, name, year);

                await Clients.Caller.SendAsync("UsersMonthlyStatisticsSort", usersMonthlyStatisticsSort);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllMovableHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllMovableHolidays.");
            }

        }

        public async Task GetUserMonthlyStatisticsEmployeeSort(int year)
        {
            try
            {

                var usersMonthlyStatisticsSort = await adminServices.GetUserMonthlyStatisticsEmployeeSort(year);

                await Clients.Caller.SendAsync("GetUserMonthlyStatisticsEmployeeSort", usersMonthlyStatisticsSort);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllMovableHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllMovableHolidays.");
            }

        }


        public async Task GetLegend(int? legendId)
        {
            try
            {
                var legend = await adminServices.Legend(legendId);
  
                    await Clients.Caller.SendAsync("Legends", legend);
  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetLegend: {ex.Message}");
                await Clients.Caller.SendAsync("Error", "An error occurred while fetching the legend.");
            }
        }


        public async Task GetBusinessOrIt()
        {
            try
            {
                var bisness = await adminServices.GetBusinessOrIt();

                await Clients.Caller.SendAsync("Business", bisness);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetLegend: {ex.Message}");
                await Clients.Caller.SendAsync("Error", "An error occurred while fetching the legend.");
            }
        }

        public async Task GetUserDetails()
        {
            try
            {
                var user = await adminServices.GetUserDetails();

                await Clients.Caller.SendAsync("UserDetails", user);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetLegend: {ex.Message}");
                await Clients.Caller.SendAsync("Error", "An error occurred while fetching the legend.");
            }
        }

        public async Task GetStatisticsEmployee()
        {
            try
            {

                var statistics = await adminServices.GetUserMonthlyStatisticsEmployee();

                await Clients.Caller.SendAsync("EmployeeStatistics", statistics);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllMovableHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllMovableHolidays.");
            }

        }



        public async Task GetLegends()
        {
            try
            {

                var legends = await adminServices.GetLegends();

                await Clients.Caller.SendAsync("GetAllLegends", legends);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in GetAllMovableHolidays: {ex.Message}");
                await Clients.Others.SendAsync("Error", "An error occurred while fetching GetAllMovableHolidays.");
            }

        }


    }
}

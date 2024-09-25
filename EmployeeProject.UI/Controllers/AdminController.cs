using EmployeeProject.Application.DTOs.Account;
using EmployeeProject.Application.DTOs.Admin;
using EmployeeProject.Application.Interfaces;
using EmployeeProject.Core.Entities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeProject.UI.Controllers
{


    public class AdminController : BaseController<AdminController>
    {
        private readonly IUserAccount services;
        private readonly IAdminServices adminServices;
        private readonly ILogger<AdminController> logger;
        public AdminController(IUserAccount services, IAdminServices adminServices, ILogger<AdminController> logger) { 
            this.services = services;   
            this.adminServices = adminServices;
            this.logger = logger;
        }

        public IActionResult Index(string success)
        {
            if (success != null) {
                TempData["Login"] = success.ToString();
                var model = new ChangePasswordDTO();
                return View(model);
            }
            else
            {

                var model = new ChangePasswordDTO();
                return View(model);
            }

        }

        public IActionResult DataSheet()
        {

            var models = new DataSheetDTO();

            return View(models);
        }
        public IActionResult Holidays()
        {

           var model = new MovableHolidaysDTO();

            return View(model);
        }

        public IActionResult Users()
        {

            var model = new AdminUsersDTO();

            return View(model);
        }

        public IActionResult Sections()
        {

            var model = new AdminUsersDTO();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AdminUsersDTO viewModel)
        {

            if(viewModel.RegisterDTO != null)
            {
                var result = await services.CreateAccount(viewModel.RegisterDTO);

                if (result)
                {
                    TempData["AddUser"] = "User Successfully Added!";
                    return RedirectToAction("Users");   
                }
                else
                {
  
                }
            }
               
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> AddSection(AdminUsersDTO viewModel)
        {

            if (viewModel.SectionDTO != null)
            {
                var result = await adminServices.AddSection(viewModel.SectionDTO);

                if (result)
                {
                    TempData["AddSection"] = "Section Successfully Added!";
                    return RedirectToAction("Users");
                }
                else
                {

                }
            }

            return RedirectToAction("Users");
        }


        [HttpPost]
        public async Task<IActionResult> AddProjects(string projectName)
        {

            if (projectName != null)
            {
                var result = await adminServices.AddProject(projectName);

                if (result)
                {
                    TempData["AddProject"] = "Project Successfully Added!";
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            return RedirectToAction("Users");
        }

        
        [HttpPost]
        public async Task<IActionResult> AddActivity(string activityName)
        {

            if (activityName != null)
            {
                var result = await adminServices.AddActivity(activityName);

                if (result)
                {
                    TempData["AddActivity"] = "Activity Successfully Added!";
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            return RedirectToAction("Users");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSection(int sectionId, string sectionName)
        {

            if (sectionId != null)
            {
                var result = await adminServices.UpdateSection(sectionId, sectionName);

                if (result)
                {
                    TempData["EditSection"] = "Section Successfully Updated!";
                    return RedirectToAction("Users");
                }
                else
                {

                }
            }

            return RedirectToAction("Users");
        }

        


        [HttpPost]
        public async Task<IActionResult> DeleteSection(int sectionId)
        {

            if (sectionId != null)
            {
                var result = await adminServices.DeleteSection(sectionId);

                if (result)
                {
                    TempData["DeleteSection"] = "Section Successfully Deleted!";
                    return RedirectToAction("Users");
                }
                else
                {

                }
            }

            return RedirectToAction("Users");
        }


        

        [HttpPost]
        public async Task<IActionResult> UpdateProject(int id, string projectName, string status)
        {

            if (id != null)
            {
                var result = await adminServices.UpdateProject(id, projectName, status);

                if (result)
                {
                    TempData["UpdateProject"] = "Project Successfully Updated!";
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            return RedirectToAction("DataSheet");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateActivity(int id, string activityName, string status)
        {

            if (id != null)
            {
                var result = await adminServices.UpdateActivity(id, activityName, status);

                if (result)
                {
                    TempData["UpdateActivity"] = "Activity Successfully Updated!";
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            return RedirectToAction("DataSheet");
        }






        [HttpPost]
        public async Task<IActionResult> UpdateAllowed(int sectionId, int sectionName)
        {

            if (sectionId != null)
            {
                var result = await adminServices.UpdateWorkingHours(sectionId, sectionName);

                if (result)
                {
                    TempData["EditAllowedHours"] = "Allowed Hours Successfully Updated!";
                    return RedirectToAction("DataSheet");
                }
                else
                {

                }
            }

            return RedirectToAction("DataSheet");
        }






        [HttpPost]
        public async Task<IActionResult> DeleteAllowed(int sectionId)
        {

            if (sectionId != null)
            {
                var result = await adminServices.DeleteWorkingHours(sectionId);

                if (result)
                {
                    TempData["DeleteAllowedHours"] = "Allowed Hours Successfully Deleted!";
                    return RedirectToAction("DataSheet");
                }
                else
                {

                }
            }

            return RedirectToAction("DataSheet");
        }








        [HttpPost]
        public async Task<IActionResult> UpdateProfile(IFormFile image)
        {
            try
            {
                var result = await adminServices.UpdateProfile(image);
                if (result)
                {
                    TempData["ProfileUpdated"] = "Profile Updated Successfully!";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while processing, in ");
                return Json(new { message = "failed" });
            }
        }




        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (userId != null)
            {
                var result = await adminServices.DeleteUser(userId);

                if (result)
                {
                    TempData["DeleteUser"] = "User Successfully De Activated!";
                    return RedirectToAction("Users");
                }
                else
                {
                    return RedirectToAction("Users");
                }

            }

            return View();
        }




        public async Task<IActionResult> AddDataSheetUserExist(string name)
        {
            if (name != null)
            {
                var (result, section) = await adminServices.AddDataSheetUserExist(name);

                if (result)
                {

                    return Json(section);
                }
                else
                {

                    return Json(section);
                }

            }


            return View();
        }

        public async Task<IActionResult> AddDataSheetUserExistEnableButton(string name)
        {
            if (name != null)
            {
                var (result, fullName, section)  = await adminServices.AddDataSheetUserExistEnableButton(name);

                if (result)
                {

                    return Json(new { result, fullName, section });
                }
                else
                {

                    return Json(new { result, fullName, section });
                }

            }

            return View();
        }

        
        //public async Task<IActionResult> CheckNumberIfExist(int number)
        //{
        //    if (number != null)
        //    {
        //        var result = await adminServices.AddDataSheetUserExist(name);

        //        if (result)
        //        {

        //            return Json("exist");
        //        }
        //        else
        //        {
        //            var messageFailed = "User Doesn't Exist! Please Add The User First!";
        //            return Json(messageFailed);
        //        }

        //    }


        //    return View();
        //}




        [HttpPost]
        public async Task<IActionResult> AddDataSheet(DataSheetDTO model)
        {
            if (model.DataSheetBusDTO != null)  
            {
                var result = await adminServices.AddDataSheet(model.DataSheetBusDTO);

                if (result)
                {
                    TempData["AddDataSheet"] = "Data Successfully Added!";
                    return RedirectToAction("DataSheet");
                }
                else
                {
                    return RedirectToAction("DataSheet");
                }

            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddWorkingHours(DataSheetDTO model)
        {
            if (model.AllowedHours != null)
            {
                var result = await adminServices.AddWorkingHours(model.AllowedHours);

                if (result)
                {
                    TempData["AddWorkingHours"] = "Working Hours Added Successfully!";
                    return RedirectToAction("DataSheet");
                }
                else
                {
                    return RedirectToAction("DataSheet");
                }

            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovable(MovableHolidaysDTO model)
        {
            if (model != null)
            {
                var result = await adminServices.AddMovable(model);

                if (result)
                {
                    TempData["AddHoliday"] = "Movable Holiday Added Successfully!";
                    return RedirectToAction("Holidays");
                }
                else
                {
                    return RedirectToAction("Holidays");
                }

            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateMovable(MovableHolidaysDTO model)
        {
            if (model != null)
            {
                var result = await adminServices.UpdateMovable(model);

                if (result)
                {
                    TempData["UpdateHoliday"] = "Movable Holiday Updated Successfully!";
                    return RedirectToAction("Holidays");
                }
                else
                {
                    return RedirectToAction("Holidays");
                }

            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteHoliday(int holidayId)
        {
            if (holidayId != null)
            {
                var result = await adminServices.DeleteMovable(holidayId);

                if (result)
                {
                    TempData["DeleteHoliday"] = "Movable Holiday Deleted Successfully!";
                    return RedirectToAction("Holidays");
                }
                else
                {
                    return RedirectToAction("Holidays");
                }
            }
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> UpdateDataSheet(DataSheetDTO model)
        {
            if (model.DataSheetBusDTO != null)
            {
                var result = await adminServices.UpdateDataSheet(model.DataSheetBusDTO);

                if (result)
                {
                    TempData["UpdateDataSheet"] = "Data Successfully Updated!";
                    return RedirectToAction("DataSheet");
                }
                else
                {
                    return RedirectToAction("DataSheet");
                }

            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteData(int dataSheetId)
        {
            if (dataSheetId != null)
            {
                var result = await adminServices.DeleteDataSheet(dataSheetId);

                if (result)
                {
                    TempData["DeleteDataSheet"] = "Data Deleted Successfully!";
                    return RedirectToAction("DataSheet");
                }
                else
                {
                    return RedirectToAction("DataSheet");
                }

            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddLegend(AdminUsersDTO viewModel)
        {
            if (viewModel.AddLegendDTO != null)
            {
                var result = await adminServices.AddLegend(viewModel.AddLegendDTO);

                if (result)
                {
                    TempData["AddLegend"] = "Legend Successfully Added!";
                    return RedirectToAction("Users");
                }
            }

            return RedirectToAction("Users");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateLegend(AdminUsersDTO viewModel)
        {
            if (viewModel.AddLegendDTO != null)
            {
                var result = await adminServices.UpdateLegend(viewModel.AddLegendDTO);

                if (result)
                {
                    TempData["UpdateLegend"] = "Legend Successfully Updated!";
                    return RedirectToAction("Users");
                }
            }

            return RedirectToAction("Users");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteLegend(int id)
        {
            if (id != null)
            {
                var result = await adminServices.DeleteLegend(id);

                if (result)
                {
                    TempData["DeleteLegend"] = "Legend Successfully Deleted!";
                    return RedirectToAction("Users");
                }
            }

            return RedirectToAction("Users");
        }

    }
}

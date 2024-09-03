using EmployeeProject.Application.DTOs.Admin;
using EmployeeProject.Application.DTOs.Employee;
using EmployeeProject.Application.Interfaces;
using EmployeeProject.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.UI.Controllers
{

    [Authorize]
    [Authorize(Policy = "RequireEmployeeRole")]
    public class EmployeeController : BaseController<EmployeeController>
    {
        private readonly IAdminServices adminServices;
        private readonly ILogger<AdminController> logger;

        public EmployeeController(IAdminServices adminServices, ILogger<AdminController> logger)
        {
            this.adminServices = adminServices;
            this.logger = logger;   
        }

        public IActionResult Index(string success)
        {
            if (success != null)
            {
                TempData["Login"] = success.ToString();
                var model = new IndexDTO();
                return View(model);
            }
            else
            {
                var model = new IndexDTO();
                return View(model);
            }

         
        }

        public IActionResult DataSheet()
        {
            return View();
        }

        public IActionResult Holidays()
        {
            return View();
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

    }
}

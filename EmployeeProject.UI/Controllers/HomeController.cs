using EmployeeProject.Application.DTOs.Account;
using EmployeeProject.Application.Interfaces;
using EmployeeProject.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.UI.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IUserAccount services;

        public HomeController(IUserAccount services)
        {
            this.services = services;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var (result, roles) = await services.ChangePassword(changePasswordDTO);

                if (result)
                {
                    ModelState.Clear();

                    if (roles == "Manager")
                    {
                        TempData["ChangePass"] = "Password Change Successfully!";
                        return RedirectToAction("Index", "Admin");

                    }
                    else if(roles == "Employee")
                    {
                        TempData["ChangePass"] = "Password Change Successfully!";
                        return RedirectToAction("Index", "Employee");
                    }

               
                }
                else
                {

                }    
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {

            if (ModelState.IsValid)
            {
                var (isLoginSuccessful, userRole, newUser) = await services.LoginAccount(loginDto);

                if (isLoginSuccessful)
                {


                    if (userRole == "Manager")
                    {
                        return RedirectToAction("Index", "Admin", new { success = "Login SuccessFully!" });
                    }
                    else if (userRole == "Project_Manager")
                    {
                        return RedirectToAction("Index", "Admin", new { success = "Login SuccessFully!" });
                    }
                    else if (userRole == "Employee")
                    {
                        return RedirectToAction("Index", "Employee", new { success = "Login SuccessFully!" });
                    }

                }
                ModelState.AddModelError("", "Invalid Login Attempt!");
                return View(loginDto);

            }

   
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var result = await services.Logout();

            if (result)
            {
                Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.Headers.Append("Pragma", "no-cache");
                Response.Headers.Append("Expires", "0");

                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}

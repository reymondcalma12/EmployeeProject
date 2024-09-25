using EmployeeProject.Core.Entities.User;
using ExternalLogin.Extensions;
using ExternalLogin.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net.Http;

namespace EmployeeProject.UI.Controllers
{
    public class ExternalController : BaseController<ExternalController>
    {
        private readonly IExternalLoginService externalLoginService;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public ExternalController(
            IExternalLoginService externalLoginService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.externalLoginService = externalLoginService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string token) // make sure to add the parameter in the endpoint
        {
            try
            {
                var ipaddress = HttpContext.IpAddress();
                var usercode = string.Empty;

                if (externalLoginService.TryValidateToken(token, ipaddress, out usercode))
                    return await AuthenticateUser(usercode); // if validated, this is where you setup the user session 

                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in Login action of ExternalController");
                return RedirectToAction("Index", "Employee");
            }
        }

        public IActionResult Logout()
        {
            try
            {
                signInManager.SignOutAsync();
                HttpContext.Session.Remove("UsersId");

                return Redirect(externalLoginService.PortalUrl);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in Logout action of ExternalController");
                return RedirectToAction("Index", "Employee");
            }
        }

        private async Task<IActionResult> AuthenticateUser(string usercode)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(usercode + "@princeretail.com");

                if (user != null)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    var userRoles = await userManager.GetRolesAsync(user);
                    var userRole = userRoles.FirstOrDefault();

                    HttpContext.Session.SetString("UsersId", user.Id);

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
                    else if (userRole == "Admin")
                    {
                        return RedirectToAction("Index", "Admin", new { success = "Login SuccessFully!" });
                    }

                }

                return Redirect(externalLoginService.PortalUrl);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in AuthenticateUser action of ExternalController");
                return RedirectToAction("Index", "Employee");
            }
        }
    }
}

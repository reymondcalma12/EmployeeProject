using EmployeeProject.Core.Entities.User;
using ExternalLogin.Extensions;
using ExternalLogin.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var ipaddress = HttpContext.IpAddress();
            var usercode = string.Empty;

            if (externalLoginService.TryValidateToken(token, ipaddress, out usercode))
                return await AuthenticateUser(usercode); // if validated, this is where you setup the user session 

            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Logout()
        {
            return Redirect(externalLoginService.PortalUrl);
        }

        // sample app user session setup 
        private async Task<IActionResult> AuthenticateUser(string usercode)
        {
            var user = await userManager.FindByEmailAsync(usercode + "@princeretail.com");

            if (user != null)
            {
                await signInManager.SignInAsync(user, isPersistent: false);


                if (user.isNewUser == true)
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var userRole = userRoles.FirstOrDefault();

                    user.isNewUser = false;

                    await userManager.UpdateAsync(user);

                    if (userRole == "Manager")
                    {
                        TempData["NewUser"] = "Yes";
                        return RedirectToAction("Index", "Admin", new { success = "Login SuccessFully!" });
                    }
                    else if (userRole == "Employee")
                    {
                        TempData["NewUser"] = "Yes";
                        return RedirectToAction("Index", "Employee", new { success = "Login SuccessFully!" });

                    }

                }
                else
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var userRole = userRoles.FirstOrDefault();

                    if (userRole == "Manager")
                    {
                        return RedirectToAction("Index", "Admin", new { success = "Login SuccessFully!" });
                    }
                    else if (userRole == "Employee")
                    {
                        return RedirectToAction("Index", "Employee", new { success = "Login SuccessFully!" });

                    }

                }


            }

            return Redirect(externalLoginService.PortalUrl);

        }
    }
}

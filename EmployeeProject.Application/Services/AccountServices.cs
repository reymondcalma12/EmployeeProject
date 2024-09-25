using EmployeeProject.Application.DTOs.Account;
using EmployeeProject.Application.Interfaces;
using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;
using EmployeeProject.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static System.Collections.Specialized.BitVector32;

namespace EmployeeProject.Application.Services
{
    public class AccountServices : IUserAccount
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IAppUnitOfWork appUnitOfWork;
        private readonly IHttpContextAccessor httpcontext;

        public AccountServices(IAppUnitOfWork _appUnitOfWork, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IHttpContextAccessor httpcontext)
        {
            this.appUnitOfWork = _appUnitOfWork;
            this.signInManager = signInManager; 
            this.userManager = userManager;
            this.httpcontext = httpcontext;
    }

        public async Task<bool> CreateAccount(RegisterDTO userDTO)
        {

            var usersId = httpcontext.HttpContext.Session.GetString("UsersId").ToString();

            var users = userManager.Users.FirstOrDefault(a => a.Id == usersId);

            if (userDTO != null)
            {
                AppUser user = new()
                {
                    FullName = userDTO.FullName,
                    UserName = userDTO.UserName,
                    Email = userDTO.UserName,
                    position = userDTO.Position,
                    SectionId = userDTO.UserRole != null ? userDTO.SectionId : users.SectionId
                };

                var result = await userManager.CreateAsync(user, userDTO.Password!);

                if (result.Succeeded)
                {

                    if (userDTO.UserRole != null)
                    {
                        await userManager.AddToRoleAsync(user, userDTO.UserRole);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Employee");
                    }

                    return true;
                }
            }

            return false;
        }

        public async Task<(bool, string, bool)> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO != null)
            {
                var result = await signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password!, false, false);
                if (result.Succeeded)
                {
                    var user = userManager.Users.FirstOrDefault(a => a.UserName == loginDTO.Username);
                    httpcontext.HttpContext.Session.SetString("UsersId", user.Id);


                    var userRoles = await userManager.GetRolesAsync(user);
                    var userRole = userRoles.FirstOrDefault();
                    if (userRole != null)
                    {
                        return (true, userRole, false);
                    }
                    
                }
            }
            return (false, null, false);
        }

    

        public async Task<bool> Logout()
        {
            await signInManager.SignOutAsync();
            httpcontext.HttpContext.Session.Remove("UsersId");
            return true;
        }

        public async Task<(bool, string)> ChangePassword(ChangePasswordDTO model)
        {

            if (model != null)
            {

                var userId = httpcontext.HttpContext.Session.GetString("UsersId");
                var user = await userManager.FindByIdAsync(userId);

                var userRoles = await userManager.GetRolesAsync(user);
                var userRole = userRoles.FirstOrDefault();

                if (user != null)
                {

                    var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    if (changePasswordResult.Succeeded)
                    {


                        return (true, userRole);
                    }
                    else
                    {
                        return (false, null);
                    }


                }
                else
                {
                    return (false, null);
                }

            }
            else
            {
                return (false, null);

            }

        }


    }
}

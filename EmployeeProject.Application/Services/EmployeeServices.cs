using EmployeeProject.Application.DTOs.Admin;
using EmployeeProject.Application.Interfaces;
using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;
using EmployeeProject.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.Application.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAppUnitOfWork appUnitOfWork;
        private readonly IHttpContextAccessor httpcontext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeServices(UserManager<AppUser> userManager, IAppUnitOfWork appUnitOfWork, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment)
        {

            this.userManager = userManager;
            this.appUnitOfWork = appUnitOfWork;
            this.httpcontext = httpcontext;
            this.webHostEnvironment = webHostEnvironment;
        }




    }
}

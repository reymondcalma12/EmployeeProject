using EmployeeProject.Application.DTOs.Account;
using EmployeeProject.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class AdminDashboardDTO
    {
        public Project Project { get; set; }

        public Activity Activity { get; set; }

    }
}

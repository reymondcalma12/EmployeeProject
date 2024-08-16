using EmployeeProject.Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class AdminDashboardDTO
    {
        public ChangePasswordDTO ChangePasswordDTO { get; set; }

        public UserMonthlyStatistics UserMonthlyStatistics { get; set; }

    }
}

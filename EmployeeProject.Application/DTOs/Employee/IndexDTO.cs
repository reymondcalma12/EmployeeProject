using EmployeeProject.Application.DTOs.Account;
using EmployeeProject.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Employee
{
    public class IndexDTO
    {
        public ChangePasswordDTO ChangePasswordDTO {  get; set; }

        public DataSheetBus DataSheetBus {  get; set; }

    }
}

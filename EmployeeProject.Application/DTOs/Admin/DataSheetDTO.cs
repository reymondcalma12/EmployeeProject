using EmployeeProject.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class DataSheetDTO
    {

        public AllowedHours AllowedHours { get; set; }

        public DataSheetBusDTO DataSheetBusDTO { get; set; }

    }
}

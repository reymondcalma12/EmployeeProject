using EmployeeProject.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class HolidayStatus
    {
        public IEnumerable<Holidays> FixedHolidays { get; set; }
        public IEnumerable<Holidays> MovableHolidays { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class HolidayStatus
    {

        [Key]
        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
}

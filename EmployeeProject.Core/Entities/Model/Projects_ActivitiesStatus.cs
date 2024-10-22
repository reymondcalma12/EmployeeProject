using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class Projects_ActivitiesStatus : BaseEntity
    {
        [Key]
        public int StatusId { get; set; } 

        public string? StatusName { get; set; }

    }
}

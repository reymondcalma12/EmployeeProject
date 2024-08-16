using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class AllowedHours : BaseEntity
    {

        [Key]
        public int AllowedHoursId { get; set; }

        public float Number { get; set; }

    }
}

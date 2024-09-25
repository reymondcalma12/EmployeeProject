using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class Activity : BaseEntity
    {

        [Key]
        public int ActivityId { get; set; }

        public string? ActivityName { get; set; }

        public string? Status { get; set; }

    }
}

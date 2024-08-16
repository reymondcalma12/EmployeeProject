using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class Holidays : BaseEntity
    {

        [Key]
        public int FixedId { get; set; }

        public string? FixedName { get; set; }

        public DateOnly FixedDate { get; set; }

        [ForeignKey("HolidayStatusId")]
        public int HolidayStatusId { get; set; }
        [ValidateNever]
        public HolidayStatus? HolidayStatus { get; set; }

    }
}

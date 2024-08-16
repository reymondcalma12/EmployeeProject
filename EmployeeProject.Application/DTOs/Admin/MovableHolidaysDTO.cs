using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class MovableHolidaysDTO
    {

        public int? Id { get; set; } 

        public string? movableName {  get; set; }

        [Required]
        public DateOnly? movableDate { get; set;}

    }
}

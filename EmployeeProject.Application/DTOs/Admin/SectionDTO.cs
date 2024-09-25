using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class SectionDTO
    {
        [Required(ErrorMessage = "Section Name is required.")]
        public string SectionName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class AddLegendDTO
    {

        public int legendID { get; set; }

        [Required]
        public int sectionId {  get; set; }

        [Required]
        public int? LOW { get; set; }

        [Required]
        public int? MED { get; set; }

        [Required]
        public int? MAX { get; set; }

    }
}

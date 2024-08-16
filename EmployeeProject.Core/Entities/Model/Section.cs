using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class Section : BaseEntity
    {
        [Key]
        public int SectionId { get; set; }

        public string? SectionName { get; set; }

    }
}

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
    public class Legend : BaseEntity
    {
        [Key]
        public int LegendId { get; set; }

        public int? LOW { get; set; }
        public int? MED { get; set; }
        public int? MAX { get; set; }

        [ForeignKey("SectionId")]
        public int SectionId { get; set; }
        [ValidateNever]
        public Section? Section { get; set; }

    }
}

using EmployeeProject.Core.Entities.User;
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
    public class DataSheetBus : BaseEntity
    {
        [Key]
        public int DataSheetBusId { get; set; }

        [ForeignKey("AppUserId")]
        public string AppUserId { get; set; }
        [ValidateNever]
        public AppUser? AppUser { get; set; }


        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        [ValidateNever]
        public Project? Project { get; set; }


        [ForeignKey("ActivityId")]
        public int ActivityId { get; set; }
        [ValidateNever]
        public Activity? Activity { get; set; }


        [ForeignKey("BusinessOrItId")]
        public int BusinessOrItId { get; set; }
        [ValidateNever]
        public BusinessOrIt? BusinessOrIt { get; set; }

        [ForeignKey("SectionId")]
        public int SectionId { get; set; }
        [ValidateNever]
        public Section? Section { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public float HoursPerDay { get; set; }

        public double HoursPerMonth { get; set; }
    }
}

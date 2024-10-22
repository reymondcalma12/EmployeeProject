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
    public class Activity : BaseEntity
    {

        [Key]
        public int ActivityId { get; set; }

        public string? ActivityName { get; set; }

        [ForeignKey("Projects_ActivitiesStatusId")]
        public int? Projects_ActivitiesStatusId { get; set; }
        [ValidateNever]
        public Projects_ActivitiesStatus? Projects_ActivitiesStatus { get; set; }

    }
}

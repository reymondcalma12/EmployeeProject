
using EmployeeProject.Core.Entities.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProject.Core.Entities.Model
{
    public class Project : BaseEntity
    {

        [Key]
        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        [ForeignKey("Projects_ActivitiesStatusId")]
        public int? Projects_ActivitiesStatusId { get; set; }
        [ValidateNever]
        public Projects_ActivitiesStatus? Projects_ActivitiesStatus { get; set; }


        [ForeignKey("AppUserId")]
        public string? AppUserId { get; set; }
        [ValidateNever]
        public AppUser? AppUser { get; set; }

    }
}

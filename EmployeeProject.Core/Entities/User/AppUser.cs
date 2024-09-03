using EmployeeProject.Core.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProject.Core.Entities.User
{
    public class AppUser : IdentityUser 
    {
        public string? FullName { get; set; }

        public bool? isNewUser { get; set; }

        public string? position { get; set; }

        public string? profileString { get; set; }

        [ForeignKey("SectionId")]
        public int? SectionId { get; set; }
        [ValidateNever]
        public Section? Section { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Account
{
    public class RegisterDTO
    {

        [Required]
        public string? FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? UserName { get; set; }

        [Required]
        public string? UserRole { get; set; }

        [Required]
        public int? SectionId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Don't Match!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

    }
}

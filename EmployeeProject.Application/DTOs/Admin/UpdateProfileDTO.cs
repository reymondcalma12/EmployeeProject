using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class UpdateProfileDTO
    {

        public IFormFile? image {  get; set; }

        public string? position { get; set; }

        public string? name { get; set; }

        public string? email { get; set; }

        public string? number{ get; set; }

    }
}

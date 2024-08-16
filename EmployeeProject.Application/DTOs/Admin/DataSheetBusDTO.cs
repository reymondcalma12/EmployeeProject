using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class DataSheetBusDTO
    {

        public int? BusId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ActivityName { get; set; }

        [Required]
        public string BusinessOrIt { get; set; }

        [Required]
        public string SectioName{ get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public float HoursPerDay { get; set; }

        [Required]
        public double HoursPerMonth { get; set; }

        public double? January { get; set; }
        public double? February { get; set; }
        public double? March { get; set; }
        public double? April { get; set; }
        public double? May { get; set; }
        public double? June { get; set; }
        public double? July { get; set; }
        public double? August { get; set; }
        public double? September { get; set; }
        public double? October { get; set; }
        public double? November { get; set; }
        public double? December { get; set; }



    }
}

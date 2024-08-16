using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Application.DTOs.Admin
{
    public class UserMonthlyStatistics
    {
        public string? AppUserId { get; set; }
        public string? AppUserName { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }

        public int Low {  get; set; }
        public int Med {  get; set; }
        public int Max {  get; set; }

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
        public double? November  { get; set; }
        public double? December { get; set; }
        public double? TotalHours { get; set; }

    }
}

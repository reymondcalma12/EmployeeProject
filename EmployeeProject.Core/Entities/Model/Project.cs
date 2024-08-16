
using System.ComponentModel.DataAnnotations;

namespace EmployeeProject.Core.Entities.Model
{
    public class Project : BaseEntity
    {

        [Key]
        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

    }
}

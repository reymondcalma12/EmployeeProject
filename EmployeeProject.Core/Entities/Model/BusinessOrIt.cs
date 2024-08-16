using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Core.Entities.Model
{
    public class BusinessOrIt : BaseEntity
    {

        [Key]
        public int BusinessOrItId { get; set; }

        public string? BusinessOrItName { get; set; }

    }
}

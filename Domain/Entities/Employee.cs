using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : Person
    {
        [Required]
        public int EmployeeNumber { get; set; }
        public string Email { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
        [ForeignKey("Area")]
        public int? IdArea { get; set; }
        public virtual Area Area { get; set; }
    }
}

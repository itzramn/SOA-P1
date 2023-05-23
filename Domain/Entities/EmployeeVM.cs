using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeVM
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmployeeNumber { get; set; }
        public string Area { get; set; }
    }
}

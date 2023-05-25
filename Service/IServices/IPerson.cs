using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IPerson
    {
        List<Person> GetPersons();
        List<EmployeeVM> GetEmployees();
        bool ValidateLogin(string email, string password);
    }
}

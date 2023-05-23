using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.DAO;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PersonService : IPerson
    {
        private readonly ILogger<PersonService> _logger;
        public readonly PersonRepository personRepository;

        public PersonService(ILogger<PersonService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            personRepository = new PersonRepository(context);
        }

        public List<Person> GetPersons()
        {
            List<Person> persons = new List<Person>();

            try
            {
                persons = personRepository.GetPersons();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return persons;
        }

        public List<EmployeeVM> GetEmployees()
        {
            List<EmployeeVM> employeeVMs = new List<EmployeeVM>();
            try
            {
                employeeVMs = personRepository.GetEmployees().Select(x => new EmployeeVM()
                {
                    Name = x.Name,
                    LastName = x.LastName,
                    Area = x.Area.Name,
                    Email = x.Email,
                    EmployeeNumber = x.EmployeeNumber.ToString()
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return employeeVMs;
        }
    }
}

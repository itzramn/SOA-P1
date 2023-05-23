using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class PersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Person> GetPersons()
        {
            List<Person> Lista = new List<Person>();

            Lista = _context.Persons.ToList();

            return Lista;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            list = _context.Employees.Include(x => x.Area).ToList();

            return list;
        }
    }
}

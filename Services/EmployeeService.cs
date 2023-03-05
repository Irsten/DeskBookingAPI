using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using System.Collections.Generic;

namespace DeskBookingAPI.Services
{
    public interface IEmployeeService
    {
        bool Create(EmployeeDto employee);
        bool Delete(Employee employeeId);
        List<Employee> GetAll();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(EmployeeDto employee)
        {
            _dbContext.Employees.Add(new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                IsAdmin = employee.IsAdmin,
            });
            _dbContext.SaveChanges();

            return true;
        }

        public List<Employee> GetAll()
        {
            var employees = _dbContext.Employees.ToList();
            return employees;
        }
        public bool Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return true;
        }
    }
}

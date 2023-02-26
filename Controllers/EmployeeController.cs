using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    [Route("/api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
        }

        [HttpPost("create")]
        public ActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto.FirstName == null) { return BadRequest("Employee first name cannot be empty."); }
            if (employeeDto.LastName == null) { return BadRequest("Employee last name cannot be empty."); }

            var process = _employeeService.Create(employeeDto);
            if(!process) { return BadRequest("Employee cannot be created."); }

            return Ok("The employee has been created.");
        }

        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            var employees = _employeeService.GetAll();

            return Ok(employees);
        }

        [HttpDelete("delete/{employeeId}")]
        public ActionResult DeleteDesk([FromRoute] int employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if(employee != null) { return BadRequest("There is no employee with this ID.");  }

            var deleting = _employeeService.Delete(employee);
            if(!deleting) { return BadRequest("Employee cannot be deleted."); }

            return Ok("Employee deleted.");
        }
    }
}

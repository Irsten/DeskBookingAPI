using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    [Route("/api/employees")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {
            // TODO
        }

        [HttpPost("create")]
        public ActionResult CreateEmployee([FromBody] string firstName, [FromBody] string lastName)
        {
            // TODO
            return Ok();
        }

        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            // TODO
            return Ok();
        }

        [HttpDelete("delete/{employeeId}")]
        public ActionResult DeleteDesk([FromRoute] int employeeId)
        {
            // TODO
            return Ok();
        }
    }
}

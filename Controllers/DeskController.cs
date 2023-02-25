using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    [Route("/api/desks")]
    public class DeskController : ControllerBase
    {
        public DeskController()
        {
            // TODO
        }
        [HttpPost("create/{roomId}")]
        public ActionResult CreateDesk([FromRoute] int roomId)
        {
            // TODO
            // Only admin
            return Ok();
        }

        [HttpGet("get-all/{roomId}")]
        public ActionResult GetAllDesksInRoom([FromRoute] int roomId)
        {
            // TODO
            return Ok();
        }

        [HttpGet("get/{deskId}")]
        public ActionResult GetDesk([FromRoute] int deskId)
        {
            // TODO
            return Ok();
        }

        [HttpDelete("delete/{deskId}")]
        public ActionResult DeleteDesk([FromRoute] int deskId) 
        {
            // TODO
            // Only admin
            return Ok();
        }

        [HttpPut("book/{deskId}")]
        public ActionResult BookDesk([FromRoute] int deskId, [FromBody] DateTime bookingDate, [FromBody] int bookingDays) 
        {
            // TODO
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    [Route("/api/rooms")]
    public class RoomController : ControllerBase
    {
        public RoomController()
        {
            // TODO
        }

        [HttpPost("create")]
        public ActionResult CreateRoom()
        {
            // TODO
            // Only admin
            return Ok();
        }
        [HttpDelete("delete/{roomId}")]
        public ActionResult DeleteRoom([FromRoute] int roomId) 
        {
            // TODO
            // Only admin
            return Ok();
        }

        [HttpGet("get-all")]
        public ActionResult GetAll() 
        {
            // TODO
            return Ok();
        }

    }
}

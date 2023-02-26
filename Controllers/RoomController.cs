using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    [Route("/api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRoomService _roomService;
        public RoomController(ApplicationDbContext dbContext, IRoomService roomService)
        {
            _dbContext = dbContext;
            _roomService = roomService;
        }

        [HttpPost("create/{employeeId}")]
        public ActionResult CreateRoom([FromRoute] int employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null) { return BadRequest("The room cannot be created because this employee does not exist."); }
            if (employee.IsAdmin == false) { return BadRequest("You have to be an administrator to creat a room."); }

            _roomService.CreateRoom();

            return Ok("The room has been created.");
        }

        [HttpDelete("delete")]
        public ActionResult DeleteRoom([FromBody] DeleteRoomDto dto) 
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("The room cannot be deleted because this employee does not exist."); }
            if (employee.IsAdmin == false) { return BadRequest("You have to be an administrator to delete a room."); }
            var room = _dbContext.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);
            if (room == null) { return BadRequest("This room does not exist."); }
            if (room.Desks != null) { return BadRequest("The room cannot be deleted if there are any desks in it."); }

            var deleting = _roomService.DeleteRoom(room);
            if (deleting == false) { return BadRequest("Room cannot be deleted"); }

            return Ok("The room has been deleted.");
        }

        [HttpGet("get-all")]
        public ActionResult GetAll() 
        {
            var rooms = _roomService.GetAll();

            return Ok(rooms);
        }

    }
}

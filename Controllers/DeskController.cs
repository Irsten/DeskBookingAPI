using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DeskBookingAPI.Controllers
{
    [Route("/api/desks")]
    public class DeskController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDeskService _deskService;
        public DeskController(ApplicationDbContext dbContext, IDeskService deskService)
        {
            _dbContext = dbContext;
            _deskService = deskService;
        }

        [HttpGet("get-all/{roomId}")]
        public ActionResult GetAllDesksInRoom([FromRoute] int roomId)
        {
            var room = _dbContext.Rooms.FirstOrDefault(r => r.Id == roomId);
            if(room == null) { return NotFound("This room does not exist."); } 

            var desks = _deskService.GetAllDesksInRoom(roomId);

            return Ok(desks);
        }

        [HttpGet("get/{deskId}")]
        public ActionResult GetDesk([FromRoute] int deskId)
        {
            var desk = _deskService.GetDesk(deskId);
            if(desk == null) { return NotFound("This desk does not exist."); }

            return Ok(desk);
        }

        [HttpPost("create")]
        public ActionResult CreateDesk([FromBody] CreateDeskDto dto)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("The desk cannot be created because this employee does not exist."); }
            if (employee.IsAdmin == false) { return BadRequest("You have to be an administrator to creat a desk."); }

            var room = _dbContext.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);
            if (room == null) { return BadRequest("The desk cannot be created because this room does not exist."); }

            _deskService.CreateDesk(room.Id);

            return Ok("The desk has been created.");
        }

        [HttpDelete("delete")]
        public ActionResult DeleteDesk([FromBody] DeleteDeskDto dto)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("The desk cannot be deleted because this employee does not exist."); }
            if (employee.IsAdmin == false) { return BadRequest("You have to be an administrator to delete a desk."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }

            _deskService.DeleteDesk(desk);

            return Ok("The desk has been deleted.");
        }
    }
}

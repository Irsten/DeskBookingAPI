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

        [HttpPut("book")]
        public ActionResult BookDesk([FromBody] BookingDto dto) 
        {
            // TODO: time validation
            if(dto.BookingDate < DateTime.Now) { return BadRequest("The desk cannot be booked in the past."); }
            if(dto.BookingDays < 0) { return BadRequest("You have to book the desk for at least 1 day."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }
            if (desk.isAvailable == false) { return BadRequest("This desk is already booked."); }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }

            var hasAlreadyBooked = _dbContext.Desks.Any(d => d.EmployeeId == dto.EmployeeId);
            if (hasAlreadyBooked) { return BadRequest("You have already booked other desk."); }



            _deskService.BookDesk(dto);

            return Ok("The desk has been booked.");
        }

        [HttpPut("cancel-reservation")]
        public ActionResult CancelBooking([FromBody] CancelReservationDto dto)
        {
            // TODO
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }
            if (desk.isAvailable == true) { return BadRequest("There is no reservation for this desk."); }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }
            if (employee.Id != desk.EmployeeId) { return BadRequest("The reservation was not made by you."); }

            _deskService.CancelReservation(desk);

            return Ok("The reservation has been cancelled.");
        }

        [HttpPut("change-reservation")]
        public ActionResult ChangeReservation([FromBody] ChangeReservationDto dto)
        {
            // TODO
            // TODO: time validation &  (try) calling existing methods
            if (dto.BookingDate < DateTime.Now) { return BadRequest("The desk cannot be booked in the past."); }
            if (dto.BookingDays < 0) { return BadRequest("You have to book the desk for at least 1 day."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }
            if (desk.isAvailable == true) { return BadRequest("There is no reservation for this desk."); }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }
            if (employee.Id != desk.EmployeeId) { return BadRequest("The reservation was not made by you."); }

            var selectedDesk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.SelectedDeskId);
            if (selectedDesk == null) { return BadRequest("Selected desk does not exist."); }
            if (selectedDesk.isAvailable == false) { return BadRequest("Selected desk is already booked."); }

            _deskService.ChangeReservation(employee, desk, selectedDesk, dto.BookingDate, dto.BookingDays);

            return Ok();
        }
        [HttpPut("change-days")]
        public ActionResult ChangeDays([FromBody] BookingDto dto)
        {
            // TODO: time validation
            if (dto.BookingDate < DateTime.Now) { return BadRequest("The desk cannot be booked in the past."); }
            if (dto.BookingDays < 0) { return BadRequest("You have to book the desk for at least 1 day."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }
            if (desk.isAvailable == true) { return BadRequest("This desk is not booked yet."); }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }
            if (employee.Id != desk.EmployeeId) { return BadRequest("The reservation was not made by you."); }

            _deskService.ChangeDays(dto);

            return Ok();
        }

        [HttpDelete("delete")]
        public ActionResult DeleteDesk([FromBody] DeleteDeskDto dto)
        {
            // TODO
            // Only admin
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("The desk cannot be deleted because this employee does not exist."); }
            if (employee.IsAdmin == false) { return BadRequest("You have to be an administrator to delete a desk."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }
            if (desk.isAvailable == false) { return BadRequest("This desk cannot be deleted because it is booked."); }

            _deskService.DeleteDesk(desk);

            return Ok("The desk has been deleted.");
        }
    }
}

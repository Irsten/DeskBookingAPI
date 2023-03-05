using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Controllers
{
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IReservationService _reservationService;
        public ReservationController(ApplicationDbContext dbContext, IReservationService reservationService)
        {
            _dbContext = dbContext;
            _reservationService = reservationService;
        }

        [HttpGet("get-all")]
        public ActionResult GetAllReservations()
        {
            var reservations = _reservationService.GetAll();

            return Ok(reservations);
        }

        [HttpPost("book-desk")]
        public ActionResult BookDesk([FromBody] BookingDto dto)
        {
            if (dto.BookingDate < DateTime.Now) { return BadRequest("The desk cannot be booked in the past."); }
            if (dto.BookingDays < 0) { return BadRequest("You have to book the desk for at least 1 day."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }
            
            var reservations = _dbContext.Reservations.Where(r => r.DeskId == dto.DeskId).ToList();
            var expirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            if(reservations != null)
            {
                foreach (var reservation in reservations)
                {
                    if ((dto.BookingDate > reservation.BookingDate && dto.BookingDate < reservation.ExpirationDate) ||
                        expirationDate > reservation.BookingDate && expirationDate < reservation.ExpirationDate)
                    {
                        return BadRequest("You cannot book a desk on this date.");
                    }
                }
            }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }

            var hasAlreadyBooked = _dbContext.Reservations.Any(d => d.EmployeeId == dto.EmployeeId);
            if (hasAlreadyBooked) { return BadRequest("You have already booked other desk."); }

            _reservationService.BookDesk(dto);

            return Ok("The desk has been booked.");
        }

        [HttpPut("change-reservation")]
        public ActionResult ChangeReservation([FromBody] ChangeReservationDto dto)
        {
            var cancelReservation = new CancelReservationDto()
            {
                DeskId = dto.CurrentDeskId,
                EmployeeId = dto.EmployeeId,
                ReservationId = dto.ReservationId,
            };
            CancelReservation(cancelReservation);

            var newReservation = new BookingDto()
            {
                EmployeeId = dto.EmployeeId,
                DeskId = dto.SelectedDeskId,
                BookingDate = dto.BookingDate,
                BookingDays = dto.BookingDays,
            };
            BookDesk(newReservation);

            return Ok("The reservation has been changed.");
        }
        [HttpPut("change-days")]
        public ActionResult ChangeDays([FromBody] ChangeDaysDto dto)
        {
            if (dto.BookingDate < DateTime.Now) { return BadRequest("The desk cannot be booked in the past."); }
            if (dto.BookingDays < 0) { return BadRequest("You have to book the desk for at least 1 day."); }

            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }

            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.EmployeeId== dto.EmployeeId);
            var expirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            if (reservation != null) { return BadRequest("This reservation does not exist."); }
            if ((dto.BookingDate > reservation.BookingDate && dto.BookingDate < reservation.ExpirationDate) ||
                    expirationDate > reservation.BookingDate && expirationDate < reservation.ExpirationDate)
            {
                return BadRequest("You cannot book a desk on this date.");
            }

            _reservationService.ChangeDays(dto);

            return Ok("Booking days have been changed.");
        }

        [HttpDelete("cancel-reservation")]
        public ActionResult CancelReservation([FromBody] CancelReservationDto dto)
        {
            // TODO
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            if (desk == null) { return BadRequest("This desk does not exist."); }

            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null) { return BadRequest("This employee does not exist."); }

            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.Id == dto.ReservationId);
            if (reservation == null) { return BadRequest("This reservation does not exist."); }
            if (reservation.EmployeeId != employee.Id) { return BadRequest("The reservation was not made by you."); }
            if (reservation.DeskId != desk.Id) { return BadRequest("There is no reservation for this desk."); }

            _reservationService.CancelReservation(reservation);

            return Ok("The reservation has been cancelled.");
        }
    }
}

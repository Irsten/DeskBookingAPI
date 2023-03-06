using DeskBookingAPI.Entities;

namespace DeskBookingAPI.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}

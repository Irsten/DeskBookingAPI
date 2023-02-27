using DeskBookingAPI.Entities;

namespace DeskBookingAPI.Models
{
    public class RoomDeskDto
    {
        public int Id { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool isAvailable { get; set; }
        public int? EmployeeId { get; set; }
        //public virtual Employee Employee { get; set; }
    }
}

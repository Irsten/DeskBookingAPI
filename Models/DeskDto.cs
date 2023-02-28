using DeskBookingAPI.Entities;

namespace DeskBookingAPI.Models
{
    public class DeskDto
    {
        public int Id { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool isAvailable { get; set; }
        public int RoomId { get; set; }
        public Employee Employee { get; set; }
    }
}

using DeskBookingAPI.Entities;

namespace DeskBookingAPI.Models
{
    public class DeskDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public List<ReservationDto> Reservations { get; set; }
    }
}

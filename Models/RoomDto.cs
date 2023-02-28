using DeskBookingAPI.Entities;

namespace DeskBookingAPI.Models
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public List<DeskDto> Desks { get; set; }
    }
}

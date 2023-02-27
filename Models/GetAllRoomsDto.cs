using DeskBookingAPI.Entities;

namespace DeskBookingAPI.Models
{
    public class GetAllRoomsDto
    {
        public int RoomId { get; set; }
        public List<RoomDeskDto> Desks { get; set; }
    }
}

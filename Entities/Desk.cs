namespace DeskBookingAPI.Entities
{
    public class Desk
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}

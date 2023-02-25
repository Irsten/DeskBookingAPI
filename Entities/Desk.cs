namespace DeskBookingAPI.Entities
{
    public class Desk
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        
    }
}

namespace DeskBookingAPI.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int DeskId { get; set; }
        public virtual Desk Desk { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

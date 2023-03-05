namespace DeskBookingAPI.Entities
{
    public class Desk
    {
        public int Id { get; set; }
        //public DateTime? BookingDate { get; set; }
        //public DateTime? ExpirationDate { get; set; }
        //public bool isAvailable { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        //public virtual List<Employee> Employees { get; set; }
        //public int? EmployeeId { get; set; }
        // public virtual Employee Employee { get; set; }

    }
}

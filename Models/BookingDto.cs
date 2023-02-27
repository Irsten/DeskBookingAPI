namespace DeskBookingAPI.Models
{
    public class BookingDto
    {
        public int DeskId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime BookingDate { get; set; }
        public int BookingDays { get; set; }
    }
}

namespace DeskBookingAPI.Models
{
    public class ChangeDaysDto
    {
        public int DeskId { get; set; }
        public int EmployeeId { get; set; }
        public int ReservationId { get; set; }
        public DateTime BookingDate { get; set; }
        public int BookingDays { get; set; }
    }
}

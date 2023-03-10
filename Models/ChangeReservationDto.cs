namespace DeskBookingAPI.Models
{
    public class ChangeReservationDto
    {
        public int EmployeeId { get; set; }
        public int CurrentDeskId { get; set; }
        public int SelectedDeskId { get; set; }
        public int ReservationId { get; set; }
        public DateTime BookingDate { get; set; }
        public int BookingDays { get; set; }
    }
}
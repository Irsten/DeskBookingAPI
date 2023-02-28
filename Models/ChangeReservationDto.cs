namespace DeskBookingAPI.Models
{
    public class ChangeReservationDto
    {
        public int EmployeeId { get; set; }
        //current
        public int DeskId { get; set; }
        public int SelectedDeskId { get; set; }

        public DateTime BookingDate { get; set; }
        public int BookingDays { get; set; }
    }
}

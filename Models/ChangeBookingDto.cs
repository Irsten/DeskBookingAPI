namespace DeskBookingAPI.Models
{
    public class ChangeBookingDto
    {
        public int EmployeeId { get; set; }
        public int DeskId { get; set; }
        public int SelectedDeskId { get; set; }
    }
}

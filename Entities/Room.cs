namespace DeskBookingAPI.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public virtual List<Desk> Desks { get; set; }
    }
}

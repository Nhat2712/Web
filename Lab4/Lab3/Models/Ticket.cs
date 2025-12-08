namespace Lab3.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? PassengerName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime TravelDate { get; set; }
        
        public decimal Price { get; set; }

    }
}

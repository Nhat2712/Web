namespace Lab8.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<CarModel> CarModels { get; set; } = new
    List<CarModel>();
    }
}

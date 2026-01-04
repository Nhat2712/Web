using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [Required]
        public int CarModelId { get; set; }
        public decimal Price { get; set; }
        
        public string? ImageUrl { get; set; }

        [ForeignKey(nameof(CarModelId))]
        public CarModel? CarModel { get; set; }
    }
}

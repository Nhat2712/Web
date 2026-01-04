using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Foreign Key 
        [ForeignKey("BrandId")] 
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }   

        // Navigation 
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}

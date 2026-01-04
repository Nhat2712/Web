using System.ComponentModel.DataAnnotations;

namespace Lab8.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }
        // Navigation
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

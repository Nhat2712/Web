using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key - Order
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        // Foreign Key - Car
        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }


        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}

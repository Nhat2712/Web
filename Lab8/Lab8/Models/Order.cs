using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        
        public decimal TotalPrice { get; set; }


        // Foreign Key
        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        // Navigation
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

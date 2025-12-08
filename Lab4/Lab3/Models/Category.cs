using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}

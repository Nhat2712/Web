using System.ComponentModel.DataAnnotations;

namespace Lab9.DTOs
{
    public class CategoryDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        public string name { get; set; } = null!;
    }
}

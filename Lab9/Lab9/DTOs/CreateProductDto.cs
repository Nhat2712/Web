using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Lab9.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm tối đa 150 ký tự")]
        public string name { get; set; } = null!;

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(1000, 100000000, ErrorMessage = "Giá phải từ 1.000 VNĐ trở lên")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Danh mục không hợp lệ")]
        public int categoryId { get; set; }

        [Required(ErrorMessage = "Ảnh sản phẩm không được để trống")]
        public IFormFile image { get; set; } = null!;
    }
}

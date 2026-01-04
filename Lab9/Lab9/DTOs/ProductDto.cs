namespace Lab9.DTOs
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public decimal price { get; set; }
        public string imageUrl { get; set; } = null!;
        public int categoryId { get; set; }
        public string categoryName { get; set; } = null!;

        public override string ToString()
        {
            return $"Sản phẩm: {name} | Giá: {price:N0} VNĐ | Danh mục: {categoryName}";
        }
    }
}

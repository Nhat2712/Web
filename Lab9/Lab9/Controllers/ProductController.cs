using Lab9.Data;
using Lab9.DTOs;
using Lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // GET /api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _db.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.Price,
                    imageUrl = p.ImageUrl,
                    categoryId = p.CategoryId,
                    categoryName = p.Category.Name
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _db.Products.Include(p => p.Category)
                                           .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound(new { message = "Sản phẩm không tồn tại" });

            return Ok(new ProductDto
            {
                id = product.Id,
                name = product.Name,
                price = product.Price,
                imageUrl = product.ImageUrl,
                categoryId = product.CategoryId,
                categoryName = product.Category.Name
            });
        }

        // POST /api/products (tạo mới, upload ảnh)
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateProductDto dto)
        {
            if (!await _db.Categories.AnyAsync(c => c.Id == dto.categoryId))
                return BadRequest(new { message = "Danh mục không tồn tại" });

            // xử lý upload ảnh
            string fileName = $"{Guid.NewGuid()}_{dto.image.FileName}";
            string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.image.CopyToAsync(stream);
            }

            var product = new Product
            {
                Name = dto.name,
                Price = dto.price,
                ImageUrl = $"uploads/{fileName}",
                CategoryId = dto.categoryId
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, new { message = "Tạo sản phẩm thành công" });
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] CreateProductDto dto)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Sản phẩm không tồn tại" });

            if (!await _db.Categories.AnyAsync(c => c.Id == dto.categoryId))
                return BadRequest(new { message = "Danh mục không tồn tại" });

            product.Name = dto.name;
            product.Price = dto.price;
            product.CategoryId = dto.categoryId;

            // nếu có ảnh mới thì update
            if (dto.image != null && dto.image.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{dto.image.FileName}";
                string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string filePath = Path.Combine(uploadFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.image.CopyToAsync(stream);
                }

                product.ImageUrl = $"uploads/{fileName}";
            }

            await _db.SaveChangesAsync();
            return Ok(new { message = "Cập nhật sản phẩm thành công" });
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Sản phẩm không tồn tại" });

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Xóa sản phẩm thành công" });
        }
    }
}

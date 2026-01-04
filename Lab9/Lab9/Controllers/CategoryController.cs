using Lab9.Data;
using Lab9.DTOs;
using Lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET /api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _db.Categories
                .Select(c => new CategoryDto { id = c.Id, name = c.Name })
                .ToListAsync();

            return Ok(categories);
        }

        // GET /api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = "Danh mục không tồn tại" });

            return Ok(new CategoryDto { id = category.Id, name = category.Name });
        }

        // POST /api/categories
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDto dto)
        {
            var exists = await _db.Categories.AnyAsync(c => c.Name == dto.name);
            if (exists)
                return BadRequest(new { message = "Tên danh mục đã tồn tại" });

            var category = new Category { Id = dto.id, Name = dto.name };
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, new { message = "Tạo danh mục thành công" });
        }

        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryDto dto)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = "Danh mục không tồn tại" });

            var exists = await _db.Categories.AnyAsync(c => c.Name == dto.name && c.Id != id);
            if (exists)
                return BadRequest(new { message = "Tên danh mục đã tồn tại cho danh mục khác" });

            category.Name = dto.name;
            await _db.SaveChangesAsync();

            return Ok(new { message = "Cập nhật danh mục thành công" });
        }

        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _db.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound(new { message = "Danh mục không tồn tại" });

            if (category.Products.Any())
                return BadRequest(new { message = "Không thể xóa danh mục đang chứa sản phẩm" });

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Xóa danh mục thành công" });
        }
    }
}

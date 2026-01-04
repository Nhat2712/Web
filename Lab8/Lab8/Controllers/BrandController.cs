using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Lab8.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: /Brand
        public IActionResult Index()
        {
            var brands = _brandService.GetAllBrands();
            return View(brands);
        }

        // GET: /Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand, IFormFile? ImageFile)
        {
            // Xử lý ảnh trước khi check ModelState
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/brands");
                Directory.CreateDirectory(imagesDir);
                var filePath = Path.Combine(imagesDir, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                ImageFile.CopyTo(stream);

                brand.ImageUrl = "/images/brands/" + fileName;
            }

            // Nếu chưa có ảnh, để giá trị mặc định
            if (string.IsNullOrEmpty(brand.ImageUrl))
                brand.ImageUrl = "/images/brands/default.png";

            // Bây giờ mới check ModelState
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                    foreach (var error in ModelState[key].Errors)
                        Console.WriteLine($"{key}: {error.ErrorMessage}");
                return View(brand);
            }

            _brandService.CreateBrand(brand);

            return RedirectToAction("Index");
        }



        // GET: /Brand/Detail/5
        public IActionResult Detail(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand == null) return NotFound();

            return View(brand);
        }

        // GET: /Brand/Edit/5
        public IActionResult Edit(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand == null) return NotFound();

            return View(brand);
        }

        // POST: /Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Brand brand, IFormFile? ImageFile)
        {
            if (id != brand.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(brand);

            // Lấy dữ liệu cũ từ DB
            var existingBrand = _brandService.GetBrandById(id);
            if (existingBrand == null) return NotFound();

            existingBrand.Name = brand.Name;
            existingBrand.Country = brand.Country;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "brands");
                Directory.CreateDirectory(imagesDir);

                var filePath = Path.Combine(imagesDir, fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    existingBrand.ImageUrl = "/images/brands/" + fileName;
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Failed to save uploaded image.");
                    return View(brand);
                }
            }

            _brandService.UpdateBrand(existingBrand);

            return RedirectToAction(nameof(Index)); // phải redirect về Index
        }

        // GET: /Brand/Delete/5
        public IActionResult Delete(int id)
        {
            _brandService.DeleteBrand(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
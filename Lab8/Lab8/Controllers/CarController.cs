using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carModelService; // cần inject thêm

        public CarController(ICarService carService, ICarModelService carModelService)
        {
            _carService = carService;
            _carModelService = carModelService;
        }

        // GET: /Car
        public IActionResult Index()
        {
            return View(_carService.GetAllCars());
        }

        // GET: /Car/Details/5
        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();

            return View(car);
        }

        // GET: /Car/Create
        public IActionResult Create()
        {
            ViewBag.CarModelList = new SelectList(_carModelService.GetCarModels(), "Id", "Name");
            return View();
        }

        // POST: /Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarModelList = new SelectList(_carModelService.GetCarModels(), "Id", "Name", car.CarModelId);
                return View(car);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");
                Directory.CreateDirectory(imagesDir);

                var filePath = Path.Combine(imagesDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                car.ImageUrl = "/images/cars/" + fileName;
            }

            _carService.CreateCar(car);
            return RedirectToAction(nameof(Index));
        }


        // GET: /Car/Edit/5
        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();

            ViewBag.CarModelList = new SelectList(_carModelService.GetCarModels(), "Id", "Name", car.CarModelId);
            return View(car);
        }

        // POST: /Car/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarModelList = new SelectList(_carModelService.GetCarModels(), "Id", "Name", car.CarModelId);
                return View(car);
            }

            var existingCar = _carService.GetCarById(car.Id);
            if (existingCar == null) return NotFound();

            existingCar.Name = car.Name;
            existingCar.CarModelId = car.CarModelId;
            existingCar.Price = car.Price;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");
                Directory.CreateDirectory(imagesDir);

                var filePath = Path.Combine(imagesDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                existingCar.ImageUrl = "/images/cars/" + fileName;
            }

            _carService.UpdateCar(existingCar);
            return RedirectToAction(nameof(Index));
        }


        // GET: /Car/Delete/5
        public IActionResult Delete(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

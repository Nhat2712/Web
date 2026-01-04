using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab8.Controllers
{
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;

        public CarModelController(ICarModelService carModelService, IBrandService brandService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
        }

        // GET: /CarModel
        public IActionResult Index()
        {
            return View(_carModelService.GetCarModels());
        }

        // GET: /CarModel/Create
        public IActionResult Create()
        {
            ViewBag.Brands = new SelectList(_brandService.GetAllBrands(), "Id", "Name");
            return View();
        }


        // POST: /CarModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
                return View(carModel);
            }

            _carModelService.CreateCarModel(carModel);
            return RedirectToAction(nameof(Index));
        }


        // GET: /CarModel/Edit/5
        public IActionResult Edit(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();

            ViewBag.Brands = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
            return View(carModel);
        }

        // POST: /CarModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarModel carModel)
        {
            if (id != carModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
                return View(carModel);
            }

            _carModelService.UpdateCarModel(carModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: /CarModel/Delete/5
        public IActionResult Delete(int id)
        {
            _carModelService.DeleteCarModel(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /CarModel/Details/5
        public IActionResult Details(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();

            return View(carModel);
        }
    }
}

using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: /Customer
        public IActionResult Index()
        {
            return View(_customerService.GetAll());
        }

        // GET: /Customer/Details/5
        public IActionResult Details(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // GET: /Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _customerService.Add(customer);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Customer/Edit/5
        public IActionResult Edit(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: /Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _customerService.Update(customer);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Customer/Delete/5
        public IActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

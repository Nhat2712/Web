using Lab8.Models;
using Lab8.Services.Implementation;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab8.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService; 
        }

        // GET: /Order
        public IActionResult Index()
        {
            return View(_orderService.GetAll());
        }

        // GET: /Order/Details/5
        public IActionResult Details(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) return NotFound();

            return View(order);
        }

        // GET: /Order/Create
        public IActionResult Create()
        {
            var customers = _customerService.GetAll(); // lấy danh sách khách hàng
            ViewBag.CustomerList = new SelectList(customers, "Id", "FullName"); // hiển thị tên đầy đủ
            return View();
        }

        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                var customers = _customerService.GetAll(); // phải set lại nếu model invalid
                ViewBag.CustomerList = new SelectList(customers, "Id", "FullName", order.CustomerId);
                return View(order);
            }

            _orderService.Add(order);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Order/Edit/5
        public IActionResult Edit(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) return NotFound();

            var customers = _customerService.GetAll();
            ViewBag.CustomerList = new SelectList(customers, "Id", "FullName", order.CustomerId);

            return View(order);
        }

        // POST: /Order/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order order)
        {
            if (!ModelState.IsValid)
            {
                var customers = _customerService.GetAll();
                ViewBag.CustomerList = new SelectList(customers, "Id", "FullName", order.CustomerId);
                return View(order);
            }

            _orderService.Update(order);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Order/Delete/5
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

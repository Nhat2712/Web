using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab8.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly ICarService _carService;

        public OrderDetailController(
            IOrderDetailService orderDetailService,
            IOrderService orderService,
            ICarService carService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _carService = carService;
        }

        // GET: /OrderDetail
        public IActionResult Index()
        {
            var details = _orderDetailService.GetAll();
            return View(details);
        }

        // GET: /OrderDetail/Details/5
        public IActionResult Details(int id)
        {
            var detail = _orderDetailService.GetById(id);
            if (detail == null) return NotFound();
            return View(detail);
        }

        // GET: /OrderDetail/Create
        public IActionResult Create()
        {
            ViewBag.OrderList = new SelectList(_orderService.GetAll(), "Id", "Id");
            ViewBag.CarList = new SelectList(_carService.GetAllCars(), "Id", "Name");
            return View();
        }

        // POST: /OrderDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderDetail detail)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OrderList = new SelectList(_orderService.GetAll(), "Id", "Id", detail.OrderId);
                ViewBag.CarList = new SelectList(_carService.GetAllCars(), "Id", "Name", detail.CarId);
                return View(detail);
            }

            _orderDetailService.Add(detail);
            return RedirectToAction(nameof(Index));
        }

        // GET: /OrderDetail/Edit/5
        public IActionResult Edit(int id)
        {
            var detail = _orderDetailService.GetById(id);
            if (detail == null) return NotFound();

            ViewBag.OrderList = new SelectList(_orderService.GetAll(), "Id", "Id", detail.OrderId);
            ViewBag.CarList = new SelectList(_carService.GetAllCars(), "Id", "Name", detail.CarId);
            return View(detail);
        }

        // POST: /OrderDetail/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderDetail detail)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OrderList = new SelectList(_orderService.GetAll(), "Id", "Id", detail.OrderId);
                ViewBag.CarList = new SelectList(_carService.GetAllCars(), "Id", "Name", detail.CarId);
                return View(detail);
            }

            _orderDetailService.Update(detail);
            return RedirectToAction(nameof(Index));
        }

        // GET: /OrderDetail/Delete/5
        public IActionResult Delete(int id)
        {
            _orderDetailService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

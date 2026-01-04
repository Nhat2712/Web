using Lab8.Data;
using Lab8.Models;
using Lab8.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lab8.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.Customer)      // nạp Customer
                .Include(o => o.OrderDetails)  // nếu cần
                .ToList();
        }

        public Order? GetById(int id)
        {
            return _context.Orders
                .Include(o => o.Customer)      // nạp Customer
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}

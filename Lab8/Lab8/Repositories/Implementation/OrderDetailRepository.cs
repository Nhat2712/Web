using Lab8.Data;
using Lab8.Models;
using Lab8.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Lab8.Repositories.Implementation
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail? GetById(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public void Add(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
            }
        }
    }
}

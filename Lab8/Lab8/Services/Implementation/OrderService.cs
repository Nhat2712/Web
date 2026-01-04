using Lab8.Models;
using Lab8.Repositories.Interface;
using Lab8.Services.Interfaces;
using System.Collections.Generic;

namespace Lab8.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public void Add(Order order)
        {
            _orderRepository.Add(order);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}

using Lab8.Models;
using Lab8.Repositories.Implementation;
using Lab8.Repositories.Interface;
using Lab8.Services.Interfaces;
using System.Collections.Generic;

namespace Lab8.Services.Implementation
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public List<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public OrderDetail? GetById(int id)
        {
            return _orderDetailRepository.GetById(id);
        }

        public void Add(OrderDetail orderDetail)
        {
            _orderDetailRepository.Add(orderDetail);
        }

        public void Update(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
        }

        public void Delete(int id)
        {
            _orderDetailRepository.Delete(id);
        }
    }
}

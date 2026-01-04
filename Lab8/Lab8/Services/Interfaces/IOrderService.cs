 using Lab8.Models;

namespace Lab8.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}

namespace Lab8.Models;

    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }


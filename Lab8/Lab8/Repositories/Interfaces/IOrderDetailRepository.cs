namespace Lab8.Models;

public interface IOrderDetailRepository
{
    List<OrderDetail> GetAll();
    OrderDetail? GetById(int id);
    void Add(OrderDetail orderDetail);
    void Update(OrderDetail orderDetail);
    void Delete(int id);
}

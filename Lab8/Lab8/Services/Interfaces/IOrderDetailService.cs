using Lab8.Models;
namespace Lab8.Services.Interfaces
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetAll();
        OrderDetail? GetById(int id);
        void Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(int id);
    }
     }

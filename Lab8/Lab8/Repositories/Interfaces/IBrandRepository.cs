using Lab8.Models;

namespace Lab8.Repositories.Interface
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
        Brand? GetById(int id);
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(int id);
    }
}

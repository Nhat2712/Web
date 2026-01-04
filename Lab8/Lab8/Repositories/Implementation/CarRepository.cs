using Lab8.Data;
using Lab8.Models;
using Lab8.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Repositories.Implementation
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Car> GetAll()
        {
            return _context.Cars
                .Include(c => c.CarModel)
                .ThenInclude(cm => cm.Brand)
                .ToList();
        }

        public Car? GetById(int id)
        {
            return _context.Cars
                .Include(c => c.CarModel)
                .ThenInclude(cm => cm.Brand)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            _context.SaveChanges();
        }
    }
}

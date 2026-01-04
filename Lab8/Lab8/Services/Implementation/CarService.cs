using Lab8.Models;
using Lab8.Repositories.Interface;
using Lab8.Services.Interfaces;

namespace Lab8.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<Car> GetAllCars()
        {
            return _carRepository.GetAll();
        }

        public Car? GetCarById(int id)
        {
            return _carRepository.GetById(id);
        }

        public void CreateCar(Car car)
        {
            _carRepository.Add(car);
        }

        public void UpdateCar(Car car)
        {
            _carRepository.Update(car);
        }

        public void DeleteCar(int id)
        {
            _carRepository.Delete(id);
        }
    }

}

using Lab8.Models;
using Lab8.Repositories.Interface;
using Lab8.Services.Interfaces;

namespace Lab8.Services.Implementation
{
    public class CarModelService : ICarModelService
    {
        private readonly ICarModelRepository _repository;

        public CarModelService(ICarModelRepository repository)
        {
            _repository = repository;
        }

        public List<CarModel> GetCarModels()
        {
            return _repository.GetAll();
        }

        public CarModel? GetCarModelById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateCarModel(CarModel carModel)
        {
            _repository.Add(carModel);
        }
        public void UpdateCarModel(CarModel carModel)
        {
            _repository.Update(carModel);
        }

        public void DeleteCarModel(int id)
        {
            _repository.Delete(id);
        }
    }
}

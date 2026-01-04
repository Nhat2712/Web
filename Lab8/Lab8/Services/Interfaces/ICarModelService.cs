using Lab8.Models;

namespace Lab8.Services.Interfaces
{
    public interface ICarModelService
    {
        List<CarModel> GetCarModels();
        CarModel? GetCarModelById(int id);
        void CreateCarModel(CarModel carModel);
        void UpdateCarModel(CarModel carModel);
        void DeleteCarModel(int id);
    }
}

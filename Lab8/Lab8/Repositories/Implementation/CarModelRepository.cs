    using Lab8.Data;
    using Lab8.Models;
    using Lab8.Repositories.Interface;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    namespace Lab8.Repositories.Implementation
    {
        public class CarModelRepository : ICarModelRepository
        {
            private readonly ApplicationDbContext _context;

            public CarModelRepository(ApplicationDbContext context)
            {
                _context = context;
            }

      
            public List<CarModel> GetAll()
            {
                return _context.CarModels
                    .Include(cm => cm.Brand) 
                    .ToList();
            }

       
            public CarModel? GetById(int id)
            {
                return _context.CarModels
                    .Include(cm => cm.Brand)
                    .FirstOrDefault(cm => cm.Id == id);
            }

            public void Add(CarModel carModel)
            {
                _context.CarModels.Add(carModel);
                _context.SaveChanges();
            }

        public void Update(CarModel carModel)
        {
            var existing = _context.CarModels.Find(carModel.Id);
            if (existing == null) return;

            existing.Name = carModel.Name;
            existing.BrandId = carModel.BrandId;
            // cập nhật các field cần thiết, KHÔNG động tới navigation

            _context.SaveChanges();
        }

        public void Delete(int id)
            {
                var carModel = _context.CarModels.Find(id);
                if (carModel != null)
                {
                    _context.CarModels.Remove(carModel);
                    _context.SaveChanges();
                }
            }
        }
    }

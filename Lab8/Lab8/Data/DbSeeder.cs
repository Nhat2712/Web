using Lab8.Data;
using Lab8.Models;
using System.Linq;

namespace Lab8
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // ✅ Seed Brand
            if (!context.Brands.Any())
            {
                var toyota = new Brand { Name = "Toyota", Country = "Japan", ImageUrl = "/images/brands/toyota.png" };
                var hyundai = new Brand { Name = "Hyundai", Country = "Korea", ImageUrl = "/images/brands/hyundai.png" };

                context.Brands.AddRange(toyota, hyundai);
                context.SaveChanges();

                // ✅ Seed CarModel
                var camryModel = new CarModel { Name = "Camry Series", BrandId = toyota.Id, Description = "Sedan hạng trung cao cấp" };
                var viosModel = new CarModel { Name = "Vios Series", BrandId = toyota.Id, Description = "Sedan nhỏ gọn" };
                var accentModel = new CarModel { Name = "Accent Series", BrandId = hyundai.Id, Description = "Sedan nhỏ gọn Hàn Quốc" };

                context.CarModels.AddRange(camryModel, viosModel, accentModel);
                context.SaveChanges();

                // ✅ Seed Car
                var cars = new[]
                {
                    new Car { Name = "Toyota Camry 2025", CarModelId = camryModel.Id, Price = 80000, ImageUrl = "/images/cars/camry.png" },
                    new Car { Name = "Toyota Vios 2025", CarModelId = viosModel.Id, Price = 25000, ImageUrl = "/images/cars/vios.png" },
                    new Car { Name = "Hyundai Accent 2025", CarModelId = accentModel.Id, Price = 23000, ImageUrl = "/images/cars/accent.png" }
                };

                context.Cars.AddRange(cars);
                context.SaveChanges();
            }
        }
    }
}

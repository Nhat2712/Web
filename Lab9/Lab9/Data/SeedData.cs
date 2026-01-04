using Lab9.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Data
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            // Seed Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Đồ uống" },
                new Category { Id = 2, Name = "Đồ ăn nhanh" },
                new Category { Id = 3, Name = "Món chính" }
            );

            // Seed Product
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Coca Cola", Price = 15000, ImageUrl = "coca.jpg", CategoryId = 1 },
                new Product { Id = 2, Name = "Pepsi", Price = 14000, ImageUrl = "pepsi.jpg", CategoryId = 1 },
                new Product { Id = 3, Name = "Khoai tây chiên", Price = 25000, ImageUrl = "fries.jpg", CategoryId = 2 },
                new Product { Id = 4, Name = "Hamburger", Price = 45000, ImageUrl = "burger.jpg", CategoryId = 2 },
                new Product { Id = 5, Name = "Cơm gà", Price = 60000, ImageUrl = "comga.jpg", CategoryId = 3 }
            );
        }
    }
}

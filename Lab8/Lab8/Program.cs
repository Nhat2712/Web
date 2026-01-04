using Lab8;
using Lab8.Data;
using Lab8.Models;
using Lab8.Repositories.Implementation;
using Lab8.Repositories.Interface;
using Lab8.Repositories.Interfaces;
using Lab8.Services.Implementation;
using Lab8.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Repositories 
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICarModelRepository, CarModelRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
//Services 
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.Seed(db);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

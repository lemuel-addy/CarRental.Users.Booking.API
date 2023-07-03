using CarRental.Users.Booking.API;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var config = builder.Configuration;

    services.AddLogging(); //A logger
    services.AddScoped<IAdminService, AdminService>(); //A service for writing
    services.AddScoped<IUserService, UserService>();//A service for reading
    //var connString = config.GetConnectionString("DbConnection");
    //services.AddNpgsql<CarRentalContext>(connString);
    services.InitializeDatabaseContext(config);


}


builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


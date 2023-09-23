using System.Text;
using AutoMapper;
using CarRental.Users.Booking.API;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Models;
using CarRental.Users.Booking.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
#nullable disable
var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var config = builder.Configuration;
    services.Configure<BearerTokenConfig>(config.GetSection(nameof(BearerTokenConfig)));
    services.AddLogging(); //A logger
    services.AddScoped<IAdminService, AdminService>(); //A service for writing
    services.AddScoped<IUserService, UserService>();//A service for reading
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IUserAccountService, UserAccountService>();
    services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    //services.AddSingleton<ICacheService, InMemoryCacheService>();

    //services.AddSingleton<IConnectionMultiplexer>(x=>
    //ConnectionMultiplexer.Connect(config.GetValue<string>("RedisConnection")));
    //services.AddSingleton<ICacheService, RedisCacheService>();
    var redisConnectionString = config.GetValue<string>("RedisConnection");
    var options = ConfigurationOptions.Parse(redisConnectionString);
    options.AbortOnConnectFail = false;

    // Create a connection multiplexer
    var connectionMultiplexer = ConnectionMultiplexer.Connect(options);

    services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
    services.AddSingleton<ICacheService, RedisCacheService>();

    //services.AddScoped<IMapper, Mapper>();
    services.AddAutoMapper(typeof(Program));
    //var connString = config.GetConnectionString("DbConnection");
    //services.AddNpgsql<CarRentalContext>(connString);
    services.InitializeDatabaseContext(config);
    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["BearerTokenConfig:Issuer"],
            ValidAudience = builder.Configuration["BearerTokenConfig:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokenConfig:SigningKey"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,         
            ValidateLifetime = true
        };
    }
    );
    builder.Services.AddAuthorization();
   


}




//builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}




//app.UseHttpsRedirection();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

//string URL = "https://localhost:7082";
//IRFAuthenticationService _rFAuthenticationService = RestService.For<IRFAuthenticationService>(URL);
//String userToken = await _rFAuthenticationService.Generate(new User
//{

//    Username = "lemueladdy",
//    Contacts = "0243662219",
//    Email = "lemuel.addy@gmail.com",
//    Password = "32GDBGHJWvd%3@",
//    Role = new Role
//    {
//        UserRole = "Admin"
//    }


//});
//Console.WriteLine(userToken);


builder.Services.AddControllers();

// Uncomment the following lines to configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

using System;
using Arch.EntityFrameworkCore.UnitOfWork;
using CarRental.Users.Booking.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Users.Booking.API
{
	public static class Startup
	{
     
        public static IServiceCollection InitializeDatabaseContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CarRentalContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DbConnection"));
            }
        , ServiceLifetime.Transient).AddUnitOfWork<CarRentalContext>();

            return services;
        }
    }
}



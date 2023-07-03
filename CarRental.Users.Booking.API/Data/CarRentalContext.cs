using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using CarRental.Users.Booking.API.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace CarRental.Users.Booking.API.Data
{
	public class CarRentalContext: DbContext
	{
        public CarRentalContext()
        {

        }



        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<CarRentalContext>().HasNoKey();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bookings> Bookings => Set<Bookings>();
        public DbSet<BookingForm> BookingForms => Set<BookingForm>();
        public DbSet<User> Users => Set<User>();

    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<CarRentalContext> //creating context
    {
        public CarRentalContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")).Build();
            var builder = new DbContextOptionsBuilder<CarRentalContext>();
            var connectionString = configuration.GetConnectionString("DbConnection");
            builder.UseNpgsql(connectionString); //using postgres
            return new CarRentalContext(builder.Options); //return context
        }
    }
}








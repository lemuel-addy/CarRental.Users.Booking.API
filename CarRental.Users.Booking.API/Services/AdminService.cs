using System;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Users.Booking.API.Services
{
    public class AdminService : IAdminService
    {

        private readonly CarRentalContext dbContext;

        public AdminService(CarRentalContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Bookings>> GetAllBookingsAsync()
        {
            return await dbContext.Bookings.AsNoTracking().ToListAsync();
        }

        public async Task<Bookings?> GetBookingAsync(int id)
        {
            return await dbContext.Bookings.FindAsync(id);
        }

        public async Task CreateBookingAsync(Bookings bookings)
        {
            dbContext.Bookings.Add(bookings);
            await dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Updates a car booking in the database
        /// </summary>
        /// <param name="updatedBookings">The bookings which will update the prior</param>
        /// <returns>Does not return anything</returns>
        public async Task UpdateBookingAsync(Bookings updatedBookings)
        {
            dbContext.Update(updatedBookings);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            await dbContext.Bookings.Where(booking => booking.Id == id)
                .ExecuteDeleteAsync();
        }

    }


}




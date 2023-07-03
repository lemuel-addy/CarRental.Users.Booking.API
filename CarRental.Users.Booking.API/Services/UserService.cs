using System;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Users.Booking.API.Services
{
    public class UserService : IUserService
    {
        private readonly CarRentalContext dbContext;

        public UserService(CarRentalContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BookingForm>> GetAllBookingFormsAsync()
        {
            return await dbContext.BookingForms.AsNoTracking().ToListAsync();
        }

        public async Task<BookingForm?> GetBookingFormAsync(int id)
        {
            return await dbContext.BookingForms.FindAsync(id);
        }

        public async Task CreateBookingFormAsync(BookingForm bookingForm)
        {
            dbContext.BookingForms.Add(bookingForm);
            await dbContext.SaveChangesAsync();

        }


        public async Task UpdateBookingFormAsync(BookingForm updatedBookingForm)
        {
            dbContext.Update(updatedBookingForm);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookingFormAsync(int id)
        {
            await dbContext.BookingForms.Where(bookingForm => bookingForm.Id == id)
                .ExecuteDeleteAsync();
        }

    }
}


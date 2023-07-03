using CarRental.Users.Booking.API.Models;

namespace CarRental.Users.Booking.API.Services
{
    public interface IUserService
    {
        Task CreateBookingFormAsync(BookingForm bookingForm);
        Task DeleteBookingFormAsync(int id);
        Task<IEnumerable<BookingForm>> GetAllBookingFormsAsync();
        Task<BookingForm?> GetBookingFormAsync(int id);
        Task UpdateBookingFormAsync(BookingForm updatedBookingForm);
    }
}
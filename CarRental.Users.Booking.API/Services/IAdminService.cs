using CarRental.Users.Booking.API.Models;

namespace CarRental.Users.Booking.API.Services
{
    public interface IAdminService
    {
        Task CreateBookingAsync(Bookings bookings);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<Bookings>> GetAllBookingsAsync();
        Task<Bookings?> GetBookingAsync(int id);
        Task UpdateBookingAsync(Bookings updatedBookings);
    }
}
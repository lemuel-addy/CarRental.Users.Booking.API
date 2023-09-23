using CarRental.Users.Booking.API.Models;

namespace CarRental.Users.Booking.API.Services
{
    public interface IAuthenticationService
    {
        public string GenerateToken(string contacts, User user);
    }
}
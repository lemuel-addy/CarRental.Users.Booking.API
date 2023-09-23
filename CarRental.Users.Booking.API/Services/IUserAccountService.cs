using CarRental.Users.Booking.API.Models;

namespace CarRental.Users.Booking.API.Services
{
    public interface IUserAccountService
    {
        Task CreateUserAsync(User user);
        Task<string> LoginAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(int id);
        Task<string> RegisterAsync(User user);


    }
}
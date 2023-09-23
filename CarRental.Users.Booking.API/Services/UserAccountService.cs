using System;
using System.Net;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Users.Booking.API.Services
{
    public class UserAccountService : IUserAccountService
    {
        //repositories
        private readonly CarRentalContext dbContext;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IAuthenticationService _authenticationService;
        public UserAccountService(CarRentalContext dbContext, IAuthenticationService authenticationService, IPasswordHasher<User> hasher)
        {
            this.dbContext = dbContext;
            _authenticationService = authenticationService;
            _hasher = hasher;
        }
        public async Task CreateUserAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await dbContext.Users.FindAsync(id);

        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            dbContext.Update(updatedUser);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await dbContext.Users.Where(user => user.Id == id)
                .ExecuteDeleteAsync();
        }


        //Actual services
        public async Task<string> LoginAsync(User user)
        {
            User? cur = await dbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == user.Email);
            if (cur is not null)
            {
                var verification = _hasher.VerifyHashedPassword(cur, cur.Password, user.Password);

                if (verification.Equals(PasswordVerificationResult.Failed))
                {
                    return "Invalid Credentials Try Again";
                }
                else
                {
                    string token = _authenticationService.GenerateToken(cur.Contacts, cur);
                    return token;

                }

            }
            else
            {
                return "Sign Up first";
            }

        }

        public async Task<string> RegisterAsync(User user)
        {
            bool cur = await dbContext.Users.AsNoTracking().AnyAsync(x => x.Email == user.Email);
            if (!cur)
            {

                await CreateUserAsync(user);
                string token = _authenticationService.GenerateToken(user.Contacts, user);
                return token;
            }
            else
            {
                return "Already registered";
            }
        }


    }
}






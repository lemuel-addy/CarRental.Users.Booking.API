using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using CarRental.Users.Booking.API.Dtos;
using CarRental.Users.Booking.API.Models;
using CarRental.Users.Booking.API.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CarRental.Users.Booking.API.Controllers
{
	public class IdentityController : ControllerBase
	{
		private const string TokenSecret = "dkajsneawudjensapeoicbvqorealsd";
		private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
		private readonly IAuthenticationService _authenticationService;
        private readonly IUserAccountService _userAccountService;
        private readonly IPasswordHasher<User> _hasher;

        public IdentityController(IAuthenticationService authenticationService, IUserAccountService userAccountService, IPasswordHasher<User> hasher)
		{
			_authenticationService = authenticationService;
            _userAccountService = userAccountService;
            _hasher = hasher;

        }

        [HttpPost("token")]
        public string Generate([FromBody] User user)
		{
			string token = _authenticationService.GenerateToken(user.Contacts, user);
			return token;
		}

		[HttpPost("login")]
		public async Task<IActionResult> UserLoginAsync([FromBody] UserDto dto)
		{
            User user = new()
            {
                Email = dto.Email,
                Password = dto.Password
            };

            


            string tokenResult = await _userAccountService.LoginAsync(user);

            return Ok(tokenResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegisterAsync([FromBody] CreateUserDto dto)
        {
            //var encrypted = _hasher.VerifyHashedPassword(user,user.Password,dto.Password)

            User user = new()
            {
                Username = dto.Username,
                Contacts = dto.Contacts,
                Email = dto.Email,
                Password = dto.Password,
                Role = new Role
                {
                    UserRole = dto.Role.UserRole
                }

            };
            user.Password = _hasher.HashPassword(user, user.Password);
            string tokenResult = await _userAccountService.RegisterAsync(user);
            return Ok(tokenResult);
        }


    }
}



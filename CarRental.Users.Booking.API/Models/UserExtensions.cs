using System;
using CarRental.Users.Booking.API.Dtos;

namespace CarRental.Users.Booking.API.Models
{
	public static class UserExtensions
	{
        public static UserDto AsDto(this User user)
        {
            return new UserDto
                (
                user.Id,
                user.Username,
                user.Contacts,
                user.Email,
                user.Password,
                user.Role

                );
        }
	}
}







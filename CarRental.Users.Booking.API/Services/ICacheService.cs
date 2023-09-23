using System;
using CarRental.Users.Booking.API.Models;

namespace CarRental.Users.Booking.API.Services
{
	public interface ICacheService
	{
        Task<string> GetCacheValueAsync(string key);
        Task SetCacheValueAsync(string key, string value);
	}
}




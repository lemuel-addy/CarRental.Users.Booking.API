﻿using System;
using Microsoft.Extensions.Caching.Memory;
#nullable disable
namespace CarRental.Users.Booking.API.Services
{
	public class InMemoryCacheService : ICacheService
	{
		private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

		public Task<string> GetCacheValueAsync(string key) 
		{
            return Task.FromResult(_cache.Get<string>(key));
		}

        public Task SetCacheValueAsync(string key, string value)
        {
			_cache.Set(key, value);
            return Task.CompletedTask;
        }
    }
}


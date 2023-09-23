using System;
#nullable disable
namespace CarRental.Users.Booking.API.Models
{
	public class BearerTokenConfig
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SigningKey { get; set; }

	}
}


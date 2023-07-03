using System;
using CarRental.Users.Booking.API.Dtos;
using CarRental.Users.Booking.API.Models;

namespace CarRental.Users.Booking.API.Models
{
	public static class BookingExtensions
	{
		public static BookingsDto AsDto(this Bookings booking)
		{
            return new BookingsDto
                (
                booking.Id,
                booking.Vehicle,
                booking.AvailabilityStartDate,
                booking.AvailabilityEndDate,
                booking.PriceperDay
                );
		}
	}
}




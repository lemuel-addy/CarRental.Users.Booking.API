using System;
using CarRental.Users.Booking.API.Dtos;

namespace CarRental.Users.Booking.API.Models
{
	public static class BookingFormExtensions
	{
        public static BookingFormDto AsDto(this BookingForm bookingForm)
        {
            return new BookingFormDto
                (
                bookingForm.Id,
                bookingForm.UserId,
                bookingForm.BookingId,
                bookingForm.PickupDate,
                bookingForm.ReturnDate


                );
        }
    }
}


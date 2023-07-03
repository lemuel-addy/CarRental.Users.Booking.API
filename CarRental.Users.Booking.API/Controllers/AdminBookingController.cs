using System;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Dtos;
using CarRental.Users.Booking.API.Models;
using CarRental.Users.Booking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Users.Booking.API.Controllers
{
    [Route("api/booking")]
    public class AdminBookingController:Controller
	{
        private readonly ILogger<AdminBookingController> _logger;
        private readonly CarRentalContext _carRentalContext;
        private readonly IAdminService _adminService;
        public AdminBookingController(ILogger<AdminBookingController> logger, CarRentalContext carRentalContext, IAdminService adminService)
        {
            _logger = logger;
            _carRentalContext = carRentalContext;
            _adminService = adminService;
        }

        [HttpGet("/admin/")]
        public async Task<IResult> Get()
        {
            var listing = (await _adminService.GetAllBookingsAsync()).Select(bookings => bookings.AsDto());
            return Results.Ok(listing);
        }

        [HttpGet("/admin/{id}")]
        public async Task<IResult> Get ([FromRoute] int id)
        {
            Bookings? booking = await _adminService.GetBookingAsync(id);
            return booking is not null ? Results.Ok(booking.AsDto()) : Results.NotFound();
        }

        [HttpPost("/admin/")]
        public async Task<IActionResult> Post([FromBody] CreateBookingsDto dto)
        {
            Bookings bookings = new()
            {
                Vehicle = dto.Vehicle,
                PriceperDay = dto.PriceperDay,
                AvailabilityStartDate = dto.AvailabilityStartDate.ToUniversalTime(),
                AvailabilityEndDate = dto.AvailabilityEndDate.ToUniversalTime()

            };


            await _adminService.CreateBookingAsync(bookings);

            return Ok(bookings);
            
        }

        [HttpPut("/admin/{id}")]
        public async Task<IResult> Put([FromRoute]int id, [FromBody] UpdateBookingsDto dto)
        {
            Bookings? bookings = await _adminService.GetBookingAsync(id);
            if (bookings is null)
            {
                return Results.NotFound();
            }


            bookings.Vehicle = dto.Vehicle;
            bookings.PriceperDay = dto.PriceperDay;
            bookings.AvailabilityStartDate = dto.AvailabilityStartDate.ToUniversalTime();
            bookings.AvailabilityEndDate = dto.AvailabilityEndDate.ToUniversalTime();
       

            await _adminService.UpdateBookingAsync(bookings);
            return Results.NoContent();
        }


        [HttpDelete("/admin/{id}")]
        public async Task<IResult> Delete([FromRoute] int id)
        {
            Bookings? bookings = await _adminService.GetBookingAsync(id);
            if (bookings is not null)
            {
                await _adminService.DeleteBookingAsync(id);
                return Results.Ok("Deleted");
            }
            return Results.NotFound();
        }

    }
}



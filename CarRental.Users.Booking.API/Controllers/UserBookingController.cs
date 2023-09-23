using System;
using CarRental.Users.Booking.API.Data;
using CarRental.Users.Booking.API.Dtos;
using CarRental.Users.Booking.API.Models;
using CarRental.Users.Booking.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
#nullable disable
namespace CarRental.Users.Booking.API.Controllers
{
    [Route("api/booking")]
    public class UserBookingController : Controller
    {
        private readonly ILogger<UserBookingController> _logger;
        private readonly CarRentalContext _carRentalContext;
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;
        public UserBookingController(ILogger<UserBookingController> logger, CarRentalContext carRentalContext, IUserService userService, ICacheService cacheService)
		{
            _logger = logger;
            _carRentalContext = carRentalContext;
            _userService = userService;
            _cacheService = cacheService;
        }


        [HttpGet("/user/")]
        public async Task<IResult> Get()
        {
            var listing = (await _userService.GetAllBookingFormsAsync()).Select(forms => forms.AsDto());
            return Results.Ok(listing);
        }

        [HttpGet("/user/{id}")]
        public async Task<IResult> Get([FromRoute] int id)
        {
            var value = await _cacheService.GetCacheValueAsync(id.ToString());
            if (string.IsNullOrEmpty(value)){
                BookingForm? bookingForm = await _userService.GetBookingFormAsync(id);
                return bookingForm is not null ? Results.Ok(bookingForm.AsDto()) : Results.NotFound();
            }
            else
            {
                //Deserialize it to JSON from string
                var bookingForm = JsonConvert.DeserializeObject<BookingForm>(value);
                return Results.Ok(bookingForm);
               
                
            }
            

            
        }

        [HttpPost("/user/")]
        public async Task<IActionResult> Post([FromBody] CreateBookingFormDto dto)
        {
            BookingForm bookingForm = new()
            {
                UserId = dto.UserId,
                BookingId = dto.BookingId,
                PickupDate = dto.PickupDate.ToUniversalTime(),
                ReturnDate = dto.ReturnDate.ToUniversalTime()

            };


            await _userService.CreateBookingFormAsync(bookingForm);
            // Serialize it to JSON string from Object
            string jsonBookingForm = JsonConvert.SerializeObject(bookingForm);

            // Save the JSON string to Redis
            await _cacheService.SetCacheValueAsync(bookingForm.Id.ToString(), jsonBookingForm);

            return Ok(bookingForm);

            
            
        }

        [HttpPut("/user/{id}")]
        public async Task<IResult> Put([FromRoute] int id, [FromBody] UpdateBookingFormDto dto)
        {
            BookingForm? bookingForm = await _userService.GetBookingFormAsync(id);
            if (bookingForm is null)
            {
                return Results.NotFound();
            }


            bookingForm.UserId = dto.UserId;
            bookingForm.BookingId = dto.BookingId;
            bookingForm.PickupDate = dto.PickupDate.ToUniversalTime();
            bookingForm.ReturnDate = dto.ReturnDate.ToUniversalTime();

            string jsonBookingForm = JsonConvert.SerializeObject(bookingForm);

            await _userService.UpdateBookingFormAsync(bookingForm);
            await _cacheService.SetCacheValueAsync(bookingForm.Id.ToString(), jsonBookingForm);
            return Results.NoContent();
        }


        [HttpDelete("/user/{id}")]
        public async Task<IResult> Delete([FromRoute] int id)
        {
            BookingForm? bookingForm = await _userService.GetBookingFormAsync(id);
            if (bookingForm is not null)
            {
                await _userService.DeleteBookingFormAsync(id);
                await _cacheService.SetCacheValueAsync(bookingForm.Id.ToString(), null);
                return Results.Ok("Deleted");
            }
            return Results.NotFound();
        }
    }


}

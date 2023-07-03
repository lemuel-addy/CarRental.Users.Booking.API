using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Users.Booking.API.Dtos;

public record UserDto
    (
        int Id,
        string Username,
        string Contacts,
        string Email,
        string Password

    );

public record CreateUserDto
    (
        [Required] string Username,
        [Required][StringLength(10)] string Contacts,
        [Required][Url] string Email,
        [Required(ErrorMessage = "Password is required")][StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")] [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")] string Password


    );

public record UpdateUserDto
    (
        [Required] string Username,
        [Required][StringLength(10)] string Contacts,
        [Required][Url] string Email,
        [Required(ErrorMessage = "Password is required")][StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")][RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")] string Password
    );

public record BookingsDto
    (
        int Id,
        string Vehicle,
        DateTime AvailabilityStartDate,
        DateTime AvailabilityEndDate,
        decimal PriceperDay

    );

public record CreateBookingsDto
    (
        [Required] string Vehicle,
        [Required] DateTime AvailabilityStartDate,
        [Required] DateTime AvailabilityEndDate,
        [Required][Range(1, 1000)] decimal PriceperDay
    );

public record UpdateBookingsDto
    (
        [Required] string Vehicle,
        [Required] DateTime AvailabilityStartDate,
        [Required] DateTime AvailabilityEndDate,
        [Required][Range(1, 1000)] decimal PriceperDay
    );


public record BookingFormDto
    (
         int Id,
         string UserId,
         string BookingId,
         DateTime PickupDate,
         DateTime ReturnDate 

    );

public record CreateBookingFormDto
    (
         [Required] string UserId,
         [Required] string BookingId,
         [Required] DateTime PickupDate,
         [Required] DateTime ReturnDate

    );


public record UpdateBookingFormDto
    (
         [Required] string UserId,
         [Required] string BookingId,
         [Required] DateTime PickupDate,
         [Required] DateTime ReturnDate

    );
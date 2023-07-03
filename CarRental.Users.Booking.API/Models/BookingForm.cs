using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace CarRental.Users.Booking.API.Models
{
    public class BookingForm
	{
        
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string BookingId { get; set; }
        [Required]
        public DateTime PickupDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}


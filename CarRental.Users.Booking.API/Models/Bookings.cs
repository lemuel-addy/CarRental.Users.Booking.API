using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace CarRental.Users.Booking.API.Models
{
    public class Bookings
	{
        
        public int Id { get; set; } 
        [Required]
        public string Vehicle { get; set; }
        [Required]
        public DateTime AvailabilityStartDate { get; set; }
        [Required]
        public DateTime AvailabilityEndDate { get; set; }
        [Required]
        [Range(1, 1000)]
        public decimal PriceperDay { get; set; }

    }
}


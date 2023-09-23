using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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


        //public override string ToString()
        //{
        //
        //   return $"Id: {Id}, UserId: {UserId}, BookingId: {BookingId}, PickupDate: {PickupDate}, ReturnDate: {ReturnDate}";
        //}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }


}


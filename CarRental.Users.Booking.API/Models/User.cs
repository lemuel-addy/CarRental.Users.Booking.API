using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace CarRental.Users.Booking.API.Models
{
	public class User
	{
        
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(10)]
        public string Contacts { get; set; }
        [Required]
        [Url]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}


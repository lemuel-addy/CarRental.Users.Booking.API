using System;
using CarRental.Users.Booking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Users.Booking.API.Data.Configurations
{
    public class BookingsConfiguration : IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder.Property(booking => booking.PriceperDay)
                .HasPrecision(5, 2);
        }
    }
}


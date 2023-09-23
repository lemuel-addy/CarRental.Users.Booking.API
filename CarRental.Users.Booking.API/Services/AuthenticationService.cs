using System;
using CarRental.Users.Booking.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace CarRental.Users.Booking.API.Services
{
	public class AuthenticationService : IAuthenticationService
	{
        private readonly BearerTokenConfig _bearerTokenConfig;
        private readonly IMapper _mapper;

        public AuthenticationService(IOptions<BearerTokenConfig> bearerTokenConfig, IMapper mapper)
		{
            _bearerTokenConfig = bearerTokenConfig.Value;
            _mapper = mapper;
        }

        public string GenerateToken(string contacts, User user)

        {

            var symmetricKey = Encoding.ASCII.GetBytes(_bearerTokenConfig.SigningKey);
            var now = DateTime.UtcNow;
            var userDetails = _mapper.Map<User>(user);

            var claims = new List<Claim>()
          {
              new Claim(ClaimTypes.NameIdentifier, user.Email),

              new Claim(ClaimTypes.MobilePhone, contacts),

              new Claim(ClaimTypes.Role, userDetails.Role.UserRole)
          };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),

                SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(

                issuer: _bearerTokenConfig.Issuer,

                audience: _bearerTokenConfig.Audience,

                expires: now.AddHours(Convert.ToInt32(24)),

                signingCredentials: signingCredentials,

                claims: claims

            );




            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}


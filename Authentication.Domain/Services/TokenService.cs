using Authentication.Domain.Entities;
using Authentication.Domain.Models;
using Authentication.Domain.Servicos.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Authentication.Domain.Servicos
{
	public class TokenService : ITokenService
	{
		private readonly ApplicationConfig _applicationConfig;

		public TokenService(ApplicationConfig applicationConfig)
		{
			_applicationConfig = applicationConfig;
		}

		public string GenerateToken(User user, DateTime expires)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_applicationConfig.JwtSecret);

			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name.Trim()) };
			claims.AddRange(user.Roles.Select(p => new Claim(ClaimTypes.Role, p.Name.Trim())));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = expires,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}

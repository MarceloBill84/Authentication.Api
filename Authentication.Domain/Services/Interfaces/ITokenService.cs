using Authentication.Domain.Entities;
using System;

namespace Authentication.Domain.Servicos.Interfaces
{
	public interface ITokenService
	{
		string GenerateToken(User user, DateTime expires);
	}
}

using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Authentication.Infra.Repositories
{
	public class LoginRegisterRepository : ILoginRegisterRepository
	{
		private readonly DbSet<LoginRegister> _dbloginRegister;
		private readonly AuthenticationContext _authenticationContext;

		public LoginRegisterRepository(AuthenticationContext authenticationContext)
		{
			_authenticationContext = authenticationContext;
			_dbloginRegister = _authenticationContext.LoginRegister;
		}

		public async Task Add(LoginRegister loginRegister)
		{
			await _dbloginRegister.AddAsync(loginRegister);
			await _authenticationContext.SaveChangesAsync();
		}
	}
}

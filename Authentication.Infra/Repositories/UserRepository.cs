using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Authentication.Infra.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DbSet<User> _dbUser;
		private readonly AuthenticationContext _authenticationContext;

		public UserRepository(AuthenticationContext authenticationContext)
		{
			_authenticationContext = authenticationContext;
			_dbUser = _authenticationContext.User;
		}

		public async Task Add(User user)
		{
			await _dbUser.AddAsync(user);
			await _authenticationContext.SaveChangesAsync();
		}

		public async Task<User> GetByName(string name)
		{
			return await _dbUser.Include(p => p.Roles).FirstOrDefaultAsync(p => p.Name == name);
		}
	}
}

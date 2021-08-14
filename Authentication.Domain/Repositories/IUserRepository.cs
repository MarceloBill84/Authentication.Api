using Authentication.Domain.Entities;
using System.Threading.Tasks;

namespace Authentication.Domain.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetByName(string name);
		Task Add(User user);
	}
}

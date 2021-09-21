using Authentication.Domain.Entities;
using System.Threading.Tasks;

namespace Authentication.Domain.Repositories
{
	public interface ILoginRegisterRepository
	{
		Task Add(LoginRegister loginRegister);
	}
}

using Authentication.Application.ViewModels;
using System.Threading.Tasks;

namespace Authentication.Application.Interfaces
{
	public interface ICreateUserAppService
	{
		Task Execute(CreateUserViewModel createUserViewModel);
	}
}

using Authentication.Application.ViewModels;
using System.Threading.Tasks;

namespace Authentication.Application.Interfaces
{
	public interface ILoginAppService
	{
		Task<TokenViewModel> Execute(LoginViewModel loginViewModel);
	}
}

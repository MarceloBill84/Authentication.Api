using Authentication.Application.Interfaces;
using Authentication.Application.ViewModels;
using Authentication.CrossCutting.Exceptions;
using Authentication.CrossCutting.Extensions;
using Authentication.Domain.Repositories;
using Authentication.Domain.Servicos.Interfaces;
using System;
using System.Threading.Tasks;

namespace Authentication.Application
{
	public class LoginAppService : ILoginAppService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITokenService _tokenService;
		private readonly ILoginRegisterRepository _loginRegisterRepository;

		public LoginAppService(IUserRepository userRepository,
			ILoginRegisterRepository loginRegisterRepository,
			ITokenService tokenService)
		{
			_userRepository = userRepository;
			_loginRegisterRepository = loginRegisterRepository;
			_tokenService = tokenService;
		}

		public async Task<TokenViewModel> Execute(LoginViewModel loginViewModel)
		{
			var user = await _userRepository.GetByName(loginViewModel.Name);

			if (user is null)
				throw new ValidationException("Usuário ou senha inválido");

			if (user.Password != loginViewModel.Password.ToHash())
				throw new ValidationException("Usuário ou senha inválido");

			await _loginRegisterRepository.Add(new(user.Id, loginViewModel.Longitude, loginViewModel.Latitude));

			var expires = DateTime.UtcNow.AddHours(8);
			var token = _tokenService.GenerateToken(user, expires);

			return new()
			{
				Token = token,
				Expires = expires
			};
		}
	}
}

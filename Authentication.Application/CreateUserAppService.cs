using Authentication.Application.Interfaces;
using Authentication.Application.ViewModels;
using Authentication.CrossCutting.Exceptions;
using Authentication.CrossCutting.Extensions;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Application
{
	public class CreateUserAppService : ICreateUserAppService
	{
		private readonly IUserRepository _userRepository;

		public CreateUserAppService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task Execute(CreateUserViewModel createUserViewModel)
		{
			var user = await _userRepository.GetByName(createUserViewModel.Name);

			if (user != null)
				throw new ValidationException("Já existe um usuário com este login");

			var roles = createUserViewModel.Roles.Select(p => new Role
			{
				Name = p
			}).ToList();

			user = new User
			{
				Name = createUserViewModel.Name,
				Password = createUserViewModel.Password.ToHash(),
				Roles = roles
			};

			await _userRepository.Add(user);
		}
	}
}

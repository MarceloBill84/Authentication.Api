using Authentication.Application;
using Authentication.Application.Interfaces;
using Authentication.Domain.Repositories;
using Authentication.Domain.Servicos;
using Authentication.Domain.Servicos.Interfaces;
using Authentication.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.CrossCutting.IoC
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ILoginRegisterRepository, LoginRegisterRepository>();
			return services;
		}

		public static IServiceCollection RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<ITokenService, TokenService>();
			return services;
		}

		public static IServiceCollection RegisterAppServices(this IServiceCollection services)
		{
			services.AddScoped<ICreateUserAppService, CreateUserAppService>()
				.AddScoped<ILoginAppService, LoginAppService>();
			return services;
		}
	}
}

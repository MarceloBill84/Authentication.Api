using Authentication.Application.Interfaces;
using Authentication.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{

		private readonly ILogger<UsersController> _logger;
		private readonly ICreateUserAppService _createUserAppService;
		private readonly ILoginAppService _loginAppService;

		public UsersController(ILogger<UsersController> logger,
			ICreateUserAppService createUserAppService,
			ILoginAppService loginAppService)
		{
			_logger = logger;
			_createUserAppService = createUserAppService;
			_loginAppService = loginAppService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel createUserViewModel)
		{
			await _createUserAppService.Execute(createUserViewModel);
			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
		{
			var token = await _loginAppService.Execute(loginViewModel);
			return Ok(token);
		}
	}
}

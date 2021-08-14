using System.Collections.Generic;

namespace Authentication.Application.ViewModels
{
	public class CreateUserViewModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}

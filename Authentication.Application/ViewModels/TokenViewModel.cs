using System;

namespace Authentication.Application.ViewModels
{
	public class TokenViewModel
	{
		public string Token { get; set; }
		public DateTime Expires { get; set; }
	}
}

using System;

namespace Authentication.Domain.Entities
{
	public class LoginRegister
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public int UserId { get; set; }
		public string Description { get; set; }

		public LoginRegister() { }
		public LoginRegister(int userId, decimal longitude, decimal latitude)
		{
			CreationDate = DateTime.UtcNow;
			UserId = userId;
			Description = $"Login registrado para longitude: {longitude} e latitude: {latitude}";
		}
	}
}

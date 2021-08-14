using System.Collections.Generic;

namespace Authentication.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public ICollection<Role> Roles { get; set; }
	}
}

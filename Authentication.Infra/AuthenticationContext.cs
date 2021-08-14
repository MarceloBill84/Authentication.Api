using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra
{
	public class AuthenticationContext : DbContext
	{
		public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
		{

		}
		public DbSet<User> User { get; set; }
		public DbSet<Role> Role { get; set; }
	}
}

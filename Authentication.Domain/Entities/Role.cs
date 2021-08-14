namespace Authentication.Domain.Entities
{
	public class Role
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }
	}
}

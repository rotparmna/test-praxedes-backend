namespace test_praxedes_backend_api.Models
{
	public class SpGetFamilyGroup
	{
		public int UserId { get; set; }
		public string DocumentNumber { get; set; }
		public string Names { get; set; }
		public string LastName { get; set; }
		public string? Gender { get; set; }
		public DateTime Birthdate { get; set; }
		public string Relationship { get; set; }
	}
}


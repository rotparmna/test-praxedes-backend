namespace test_praxedes_backend_api.Dto
{
	public class UserDto
	{
		public int UserId { get; set; }
		public string DocumentNumber { get; set; }
		public string Names { get; set; }
		public string LastName { get; set; }
		public string? Gender { get; set; }
        public DateTime Birthdate { get; set; }
	}
}


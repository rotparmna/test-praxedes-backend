namespace test_praxedes_backend_api.Models
{
	public class Comment
	{
		public int IdComment { get; set; }
		public int IdPost { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

		public Post? Post { get; set; }
	}
}


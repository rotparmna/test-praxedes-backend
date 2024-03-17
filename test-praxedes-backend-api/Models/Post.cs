namespace test_praxedes_backend_api.Models
{
	public class Post
	{
		public int IdPost { get; set; }
		public int IdUser { get; set; }
		public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

		public List<Comment> Comments { get; set; } = new List<Comment>();
	}
}


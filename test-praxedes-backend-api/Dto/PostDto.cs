namespace test_praxedes_backend_api.Dto
{
	public class PostDto
	{
		public int IdPost { get; set; }
		public int IdUser { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
    }
}


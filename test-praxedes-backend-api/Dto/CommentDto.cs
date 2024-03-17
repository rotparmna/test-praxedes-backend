namespace test_praxedes_backend_api.Dto
{
	public class CommentDto
	{
		public int IdComment { get; set; }
		public int IdPost { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}


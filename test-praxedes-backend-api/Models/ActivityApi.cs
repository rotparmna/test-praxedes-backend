namespace test_praxedes_backend_api.Models
{
	public class ActivityApi
	{
		public string IdActivityApi { get; set; } = string.Empty;
		public string Resource { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string HttpStatusCode { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
    }
}


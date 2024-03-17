namespace test_praxedes_backend_api.Dto
{
    public class JsonErrorResponse
    {
        public string[] Messages { get; set; } = Array.Empty<string>();
        public string DeveloperMessage { get; set; } = string.Empty;
    }
}


namespace test_praxedes_backend_api.Exceptions
{
	public class ValidatorException:Exception, IDataErrorException
    {
        public List<string> DataError { get; set; } = new List<string>();

        public ValidatorException(string message, Exception innerException)
            : base(message, innerException)
        {
		}

        public ValidatorException(string message)
            : base(message)
        {
        }
    }
}


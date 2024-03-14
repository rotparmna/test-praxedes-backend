namespace test_praxedes_backend_api.Exceptions
{
    public class UniqueConstraintException : Exception
	{
		public UniqueConstraintException(string message, Exception innerException)
            : base(message, innerException)
		{
		}
	}
}


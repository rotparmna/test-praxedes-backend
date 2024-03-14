namespace test_praxedes_backend_api.Contracts
{
	public interface IExecuteStoreProcedure<T, R>
	{
        T Execute(R input);
    }
}


using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface ISpGetCommentsByIdPost : IExecuteStoreProcedure<Task<List<Comment>>, int>
    {
	}
}


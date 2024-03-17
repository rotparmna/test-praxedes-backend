using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface ISpGetPosts : IExecuteStoreProcedure<Task<List<Post>>, string>
    {
    }
}


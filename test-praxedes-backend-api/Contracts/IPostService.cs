using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface IPostService
	{
		Task<Post> GetPostById(int idPost);
		Task<List<Post>> GetAllPost();
		Task CreatePost(Post post);
		Task UpdatePost(Post post);
		Task DeletePost(int idPost);
		Task Bulk();
	}
}


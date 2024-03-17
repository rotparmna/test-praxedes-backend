using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface ICommentService
	{
		Task CreateComment(Comment comment);
		Task UpdateComment(Comment comment);
		Task DeleteComment(int idComment);
        Task<Comment> GetCommentById(int idComment);
		Task<List<Comment>> GetComments();
		Task<List<Comment>> GetCommentsByPost(int idPost);
	}
}


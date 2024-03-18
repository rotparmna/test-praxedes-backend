using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
    public class CommentService : ICommentService
	{
        private readonly ISpInsertComment spInsertComment;
        private readonly ISpUpdateComment spUpdateComment;
        private readonly ISpDeleteComment spDeleteComment;
        private readonly ISpGetCommentById spGetCommentById;
        private readonly ISpGetComments spGetComments;
        private readonly ISpGetCommentsByIdPost spGetCommentsByIdPost;

        public CommentService(ISpInsertComment spInsertComment,
            ISpUpdateComment spUpdateComment,
            ISpDeleteComment spDeleteComment,
            ISpGetCommentById spGetCommentById,
            ISpGetComments spGetComments,
            ISpGetCommentsByIdPost spGetCommentsByIdPost)
		{
            this.spInsertComment = spInsertComment;
            this.spUpdateComment = spUpdateComment;
            this.spDeleteComment = spDeleteComment;
            this.spGetCommentById = spGetCommentById;
            this.spGetComments = spGetComments;
            this.spGetCommentsByIdPost = spGetCommentsByIdPost;
        }

        public async Task CreateComment(Comment comment)
        {
            await spInsertComment.Execute(comment);
        }

        public async Task DeleteComment(int idComment)
        {
            await spDeleteComment.Execute(idComment);
        }

        public async Task<Comment> GetCommentById(int idComment)
        {
            return await spGetCommentById.Execute(idComment);
        }

        public async Task<List<Comment>> GetComments()
        {
            return await spGetComments.Execute();
        }

        public async Task<List<Comment>> GetCommentsByPost(int idPost)
        {
            return await spGetCommentsByIdPost.Execute(idPost);
        }

        public async Task UpdateComment(Comment comment)
        {
            await spUpdateComment.Execute(comment);
        }
    }
}


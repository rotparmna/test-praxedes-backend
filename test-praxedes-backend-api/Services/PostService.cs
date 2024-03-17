using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
    public class PostService : IPostService
	{
        private readonly ISpDeletePost spDeletePost;
        private readonly ISpGetPostById spGetPostById;
        private readonly ISpGetPosts spGetPosts;
        private readonly ISpInsertPost spInsertPost;
        private readonly ISpUpdatePost spUpdatePost;

        public PostService(ISpDeletePost spDeletePost,
            ISpGetPostById spGetPostById,
            ISpGetPosts spGetPosts,
            ISpInsertPost spInsertPost,
            ISpUpdatePost spUpdatePost)
		{
            this.spDeletePost = spDeletePost;
            this.spGetPostById = spGetPostById;
            this.spGetPosts = spGetPosts;
            this.spInsertPost = spInsertPost;
            this.spUpdatePost = spUpdatePost;
        }

        public async Task CreatePost(Post post)
        {
            await spInsertPost.Execute(post);
        }

        public async Task DeletePost(int idPost)
        {
            await spDeletePost.Execute(idPost);
        }

        public async Task<List<Post>> GetAllPost()
        {
            return await spGetPosts.Execute();
        }

        public async Task<Post> GetPostById(int idPost)
        {
            return await spGetPostById.Execute(idPost);
        }

        public async Task UpdatePost(Post post)
        {
            await spUpdatePost.Execute(post);
        }
    }
}


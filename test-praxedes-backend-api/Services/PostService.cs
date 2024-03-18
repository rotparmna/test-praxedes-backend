using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;
using System.Linq;

namespace test_praxedes_backend_api.Services
{
    public class PostService : IPostService
    {
        private readonly ISpDeletePost spDeletePost;
        private readonly ISpGetPostById spGetPostById;
        private readonly ISpGetPosts spGetPosts;
        private readonly ISpInsertPost spInsertPost;
        private readonly ISpUpdatePost spUpdatePost;
        private readonly IJsonPlaceholderService jsonPlaceholderService;
        private readonly IInsertBulkPostComment insertBulkPostComment;

        public PostService(ISpDeletePost spDeletePost,
            ISpGetPostById spGetPostById,
            ISpGetPosts spGetPosts,
            ISpInsertPost spInsertPost,
            ISpUpdatePost spUpdatePost,
            IJsonPlaceholderService jsonPlaceholderService,
            IInsertBulkPostComment insertBulkPostComment)
		{
            this.spDeletePost = spDeletePost;
            this.spGetPostById = spGetPostById;
            this.spGetPosts = spGetPosts;
            this.spInsertPost = spInsertPost;
            this.spUpdatePost = spUpdatePost;
            this.jsonPlaceholderService = jsonPlaceholderService;
            this.insertBulkPostComment = insertBulkPostComment;
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

        public async Task Bulk()
        {
            var posts = new List<Post>();
            var placeholdersPost = await jsonPlaceholderService.GetPosts();
            placeholdersPost.ForEach(x =>
            {
                posts.Add(new Post()
                {
                    IdPost = x.id,
                    IdUser = x.userId,
                    Title = x.title,
                    Body = x.body
                });
            });
            var comments = new List<Comment>();
            var placeholderComment = await jsonPlaceholderService.GetComments();
            placeholderComment.ForEach(x =>
            {
                comments.Add(new Comment()
                {
                    IdComment = x.id,
                    IdPost = x.postId,
                    Name = x.name,
                    Email = x.email,
                    Body = x.body
                });
            });
            await insertBulkPostComment.InsertBulk(posts, comments);

        }
    }
}


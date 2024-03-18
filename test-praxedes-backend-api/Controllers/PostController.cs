using Microsoft.AspNetCore.Mvc;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Dto;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
		public PostController(IPostService postService)
		{
			this.postService = postService;
		}

        [HttpPost]
        [Route("bulk")]
        public async Task<IResult> Bulk()
        {
            await postService.Bulk();
            return Results.Ok();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IResult> GetPosts()
        {
            return Results.Ok(await postService.GetAllPost());
        }

        [HttpGet]
        public async Task<IResult> GetPostById(int? idPost)
        {
            if (!idPost.HasValue)
                return Results.BadRequest();
            return Results.Ok(await postService.GetPostById(idPost.Value));
        }

        [HttpPost]
        public async Task<IResult> Createpost([FromForm] PostDto post)
        {
            await postService.CreatePost(new Post()
            {
                Body = post.Body,
                Title = post.Title,
                IdUser = post.IdUser
            });

            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> UpdatePost(PostDto post)
        {
            await postService.UpdatePost(new()
            {
                IdPost = post.IdPost,
                Body = post.Body,
                Title = post.Title,
                IdUser = post.IdUser
            });

            return Results.Ok();
        }

        [HttpDelete]
        public async Task<IResult> DeletePost(int? idPost)
        {
            if (!idPost.HasValue)
                return Results.BadRequest();
            await postService.DeletePost(idPost.Value);
            return Results.Ok();
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Dto;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
		public CommentController(ICommentService commentService)
		{
            this.commentService = commentService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IResult> GetComments()
        {
            return Results.Ok(await commentService.GetComments());
        }

        [HttpGet]
        [Route("getByPost")]
        public async Task<IResult> GetCommentsByPost(int? idPost)
        {
            if (!idPost.HasValue)
                return Results.BadRequest();
            var commentsDto = new List<CommentDto>();
            var comments = await commentService.GetCommentsByPost(idPost.Value);
            comments.ForEach(x =>
            {
                commentsDto.Add(new()
                {
                    Email = x.Email,
                    Name = x.Name,
                    IdComment = x.IdComment,
                    IdPost = x.IdPost,
                    Body = x.Body
                });
            });
            return Results.Ok(commentsDto);
        }

        [HttpGet]
        public async Task<IResult> GetCommentById(int? idPost)
        {
            if (!idPost.HasValue)
                return Results.BadRequest();
            return Results.Ok(await commentService.GetCommentById(idPost.Value));
        }

        [HttpPost]
        public async Task<IResult> CreateComment([FromForm] CommentDto dto)
        {
            await commentService.CreateComment(new Comment()
            {
                IdPost = dto.IdPost,
                Name = dto.Name,
                Email = dto.Email,
                Body = dto.Body
            });

            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> UpdateComment(CommentDto dto)
        {
            await commentService.UpdateComment(new()
            {
                IdPost = dto.IdPost,
                Name = dto.Name,
                Email = dto.Email,
                Body = dto.Body,
                IdComment = dto.IdComment
            });

            return Results.Ok();
        }

        [HttpDelete]
        public async Task<IResult> DeleteComment(int? idComment)
        {
            if (!idComment.HasValue)
                return Results.BadRequest();
            await commentService.DeleteComment(idComment.Value);
            return Results.Ok();
        }
    }
}


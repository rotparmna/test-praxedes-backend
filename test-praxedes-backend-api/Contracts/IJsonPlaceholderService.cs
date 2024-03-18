using test_praxedes_backend_api.Dto;

namespace test_praxedes_backend_api.Contracts
{
    public interface IJsonPlaceholderService
    {
        Task<List<JsonPlaceholderPostDto>> GetPosts();
        Task<List<JsonPlaceholderCommentDto>> GetComments();
    }
}
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;
using Z.Dapper.Plus;

namespace test_praxedes_backend_api.Infraestructure
{
    public class InsertBulkPostComment : IInsertBulkPostComment
    {
        private readonly SqlConnectionFactory connection;

        public InsertBulkPostComment(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task InsertBulk(List<Post> posts, List<Comment> comments)
        {
            connection.Create().BulkInsert(posts);
            connection.Create().BulkInsert(comments);
        }
    }
}


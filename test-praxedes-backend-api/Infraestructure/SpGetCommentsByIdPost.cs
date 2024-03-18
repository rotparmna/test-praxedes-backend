using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetCommentsByIdPost : ISpGetCommentsByIdPost
    {
        private readonly SqlConnectionFactory connection;

        public SpGetCommentsByIdPost(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<List<Comment>> Execute(int input)
        {
            return (await connection.Create().QueryAsync<Comment>("spGetCommentsByIdPost",
                new { IdPost = input },
                commandType: CommandType.StoredProcedure))
                .ToList();
        }

        public Task<List<Comment>> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


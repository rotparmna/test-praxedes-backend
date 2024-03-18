using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetCommentById : ISpGetCommentById
	{
        private readonly SqlConnectionFactory connection;

        public SpGetCommentById(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<Comment> Execute(int input)
        {
            return (await connection.Create().QueryAsync<Comment>("spGetCommentById",
                new { IdComment = input },
                commandType: CommandType.StoredProcedure))
                .DefaultIfEmpty(new Comment()
                {
                    IdComment = -1
                })
                .FirstOrDefault();
        }

        public Task<Comment> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


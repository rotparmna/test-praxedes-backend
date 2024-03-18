using System;
using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpUpdateComment : ISpUpdateComment
	{
        private readonly SqlConnectionFactory connection;

        public SpUpdateComment(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(Comment input)
        {
            await connection.Create().ExecuteAsync("spUpdateComment",
                new
                {
                    input.IdComment,
                    input.IdPost,
                    input.Name,
                    input.Email,
                    input.Body
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


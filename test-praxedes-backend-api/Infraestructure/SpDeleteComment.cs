using System;
using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpDeleteComment : ISpDeleteComment
	{
        private readonly SqlConnectionFactory connection;

        public SpDeleteComment(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(int input)
        {
            await connection.Create().ExecuteAsync("spDeleteComment",
                new
                {
                    IdComment = input
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


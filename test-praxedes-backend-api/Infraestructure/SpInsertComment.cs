using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpInsertComment : ISpInsertComment
	{
        private readonly SqlConnectionFactory connection;

        public SpInsertComment(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(Comment input)
        {
            await connection.Create().ExecuteAsync("spInsertComment",
                    new
                    {
                        input.IdPost,
                        input.Name,
                        input.Body,
                        input.Email
                    },
                    commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


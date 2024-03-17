using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpInsertPost : ISpInsertPost
	{
        private readonly SqlConnectionFactory connection;

        public SpInsertPost(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(Post input)
        {
            await connection.Create().ExecuteAsync("spInsertPost",
                    new
                    {
                        input.IdUser,
                        input.Title,
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


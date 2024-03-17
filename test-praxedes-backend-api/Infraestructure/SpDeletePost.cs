using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpDeletePost : ISpDeletePost
    {
        private readonly SqlConnectionFactory connection;

        public SpDeletePost(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(int input)
        {
            await connection.Create().ExecuteAsync("spDeletepost",
                new
                {
                    IdPost = input
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


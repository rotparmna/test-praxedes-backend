using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetPosts : ISpGetPosts
    {
        private readonly SqlConnectionFactory connection;

        public SpGetPosts(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public Task<List<Post>> Execute(string input)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> Execute()
        {
            return (await connection.Create().QueryAsync<Post>("spGetPosts",
                commandType: CommandType.StoredProcedure))
                .ToList();
        }
    }
}


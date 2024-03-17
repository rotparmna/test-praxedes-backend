using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetPostById : ISpGetPostById
	{
        private readonly SqlConnectionFactory connection;

        public SpGetPostById(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<Post> Execute(int input)
        {
            return (await connection.Create().QueryAsync<Post>("spGetPostById",
                new { PostId = input },
                commandType: CommandType.StoredProcedure))
                .DefaultIfEmpty(new Post()
                {
                    IdPost = -1
                })
                .FirstOrDefault();
        }

        public Task<Post> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


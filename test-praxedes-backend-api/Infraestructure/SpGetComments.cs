using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetComments : ISpGetComments
	{
        private readonly SqlConnectionFactory connection;

        public SpGetComments(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public Task<List<Comment>> Execute(string input)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> Execute()
        {
            return (await connection.Create().QueryAsync<Comment>("spGetComments",
                commandType: CommandType.StoredProcedure))
                .ToList();
        }
    }
}


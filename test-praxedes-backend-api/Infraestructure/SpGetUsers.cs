using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetUsers : ISpGetUsers
	{
        private readonly SqlConnectionFactory connection;

        public SpGetUsers(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public Task<List<User>> Execute(string input)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> Execute()
        {
            return (await connection.Create().QueryAsync<User>("spGetUsers",
                commandType: CommandType.StoredProcedure))
                .ToList();
        }
    }
}


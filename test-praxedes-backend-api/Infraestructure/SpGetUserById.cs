using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetUserById : ISpGetUserById
	{
        private readonly SqlConnectionFactory connection;

        public SpGetUserById(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<User> Execute(int input)
        {
            return (await connection.Create().QueryAsync<User>("spGetUserById",
                new { UserId = input },
                commandType: CommandType.StoredProcedure))
                .DefaultIfEmpty(new User()
                {
                    UserId = -1
                })
                .FirstOrDefault();
        }

        public Task<User> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


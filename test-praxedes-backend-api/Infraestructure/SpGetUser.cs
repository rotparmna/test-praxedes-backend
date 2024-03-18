using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetUser : ISpGetUser
    {
        private readonly SqlConnectionFactory connection;

        public SpGetUser(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<Models.User> Execute(string documentNumber)
        {
            return (await connection.Create().QueryAsync<Models.User>("spGetUser",
                new { DocumentNumber = documentNumber },
                commandType: CommandType.StoredProcedure))
                .DefaultIfEmpty(new Models.User()
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


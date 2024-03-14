using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpUpdateUser : ISpUpdateUser
    {
        private readonly SqlConnectionFactory connection;

        public SpUpdateUser(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(User input)
        {
            await connection.Create().ExecuteAsync("spUpdateUser",
                new
                {
                    input.UserId,
                    input.Names,
                    input.LastName,
                    input.Gender,
                    input.Birthdate
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


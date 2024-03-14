using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpInsertUser : ISpInsertUser
    {
        private readonly SqlConnectionFactory connection;

        public SpInsertUser(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<int> Execute(User user)
        {
            return (await connection.Create().ExecuteScalarAsync<int>("spInsertUserRelationship",
                new
                {
                    user.DocumentNumber,
                    user.Names,
                    user.LastName,
                    user.Gender,
                    user.Birthdate
                },
                commandType: CommandType.StoredProcedure));
        }

        public Task<int> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


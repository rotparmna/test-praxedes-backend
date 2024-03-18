using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetFamilyGroup : ISpGetFamilyGroup
    {
        private readonly SqlConnectionFactory  connection;

        public SpGetFamilyGroup(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task<List<Models.SpGetFamilyGroup>> Execute(int userId)
        {
            return (await connection.Create().QueryAsync<Models.SpGetFamilyGroup>("spGetFamilyGroup",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure))
                .ToList();
        }

        public Task<List<Models.SpGetFamilyGroup>> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


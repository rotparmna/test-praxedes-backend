using System.Data;
using Dapper;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpInsertUserRelationship : ISpInsertUserRelationship
    {
        private readonly SqlConnectionFactory connection;

        public SpInsertUserRelationship(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(FamilyGroup input)
        {
            await connection.Create().ExecuteAsync("spInsertUserRelationship",
                new
                {
                    UserIdParent = input.UserParent.UserId,
                    UserIdChild = input.UserChild.UserId,
                    input.Relationship
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}


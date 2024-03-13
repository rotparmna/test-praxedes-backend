using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using test_praxedes_backend_api.Contracts;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpGetFamilyGroup : ISpGetFamilyGroup
    {
        private readonly IConfiguration? configuration;

        public SpGetFamilyGroup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Models.SpGetFamilyGroup>> Execute(int userId)
        {
            var connectionString = configuration?.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            return (await connection.QueryAsync<Models.SpGetFamilyGroup>("spGetFamilyGroup",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure))
                .ToList();
        }
    }
}


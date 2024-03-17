using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpUpdateActivityApi : ISpUpdateActivityApi
    {
        private readonly SqlConnectionFactory connection;

        public SpUpdateActivityApi(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(ActivityApi input)
        {
            await connection.Create().ExecuteAsync("spUpdateActivityApi",
                new
                {
                    input.IdActivityApi,
                    input.Exception,
                    input.HttpStatusCode
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


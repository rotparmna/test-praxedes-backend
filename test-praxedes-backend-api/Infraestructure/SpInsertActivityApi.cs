using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Infraestructure
{
    public class SpInsertActivityApi : ISpInsertActivityApi
    {
        private readonly SqlConnectionFactory connection;

        public SpInsertActivityApi(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(ActivityApi input)
        {
            await connection.Create().ExecuteAsync("SpInsertActivityApi",
                new
                {
                    input.IdActivityApi,
                    input.Exception,
                    input.HttpStatusCode,
                    input.Method,
                    input.Path,
                    input.Resource
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


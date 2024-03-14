using Dapper;
using System.Data;
using test_praxedes_backend_api.Contracts;

namespace test_praxedes_backend_api.Infraestructure
{
	public class SpDeleteUser:ISpDeleteUser
	{
        private readonly SqlConnectionFactory connection;

        public SpDeleteUser(SqlConnectionFactory connection)
        {
            this.connection = connection;
        }

        public async Task Execute(int input)
        {
            await connection.Create().ExecuteAsync("spDeleteUser",
                new
                {
                    UserId = input
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}


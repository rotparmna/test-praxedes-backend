using Microsoft.Data.SqlClient;

namespace test_praxedes_backend_api.Infraestructure
{
	public class SqlConnectionFactory
	{
		private readonly string connectionString;

		public SqlConnectionFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public SqlConnection Create()
		{
			return new SqlConnection(connectionString);
		}
	}
}


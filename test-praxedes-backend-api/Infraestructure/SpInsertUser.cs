using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Exceptions;
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
            try
            {
                return (await connection.Create().ExecuteScalarAsync<int>("spInsertUser",
                    new
                    {
                        user.DocumentNumber,
                        user.Names,
                        user.LastName,
                        user.Gender,
                        user.Birthdate,
                        user.UserId
                    },
                    commandType: CommandType.StoredProcedure));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) 
                {
                    throw new UniqueConstraintException("El usuario " + user.DocumentNumber + " ya se encuentra registrado.", ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public Task<int> Execute()
        {
            throw new NotImplementedException();
        }
    }
}


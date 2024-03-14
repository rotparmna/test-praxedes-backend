using Microsoft.Data.SqlClient;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Exceptions;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
    public class UserService : IUserService
	{
        private readonly ISpGetUserById spGetUserById;
        private readonly ISpGetUsers spGetUsers;
        private readonly ISpInsertUser spInsertUser;
        private readonly ISpDeleteUser spDeleteUser;
        private readonly ISpUpdateUser spUpdateUser;
        private readonly ILogger<UserService> logger;

        public UserService(ISpGetUserById spGetUserById,
            ISpGetUsers spGetUsers,
            ISpInsertUser spInsertUser,
            ISpDeleteUser spDeleteUser,
            ISpUpdateUser spUpdateUser,
            ILogger<UserService> logger)
		{
            this.spGetUserById = spGetUserById;
            this.spGetUsers = spGetUsers;
            this.spInsertUser = spInsertUser;
            this.spDeleteUser = spDeleteUser;
            this.spUpdateUser = spUpdateUser;
            this.logger = logger;
        }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                await spInsertUser.Execute(user);
                return true;
            }
            catch(UniqueConstraintException ex)
            {
                logger.LogError(ex, "Se intentó crear un usuario con un numero de documento existente, número de documento " + user.DocumentNumber);
                return false;
            }
            catch(SqlException sqlEx)
            {
                logger.LogError(sqlEx, "Error inesperado en la creación del usuario, número de documento " + user.DocumentNumber);
                return false;
            }
        }

        public async Task<User> GetUserById(int idUser)
        {
            return await spGetUserById.Execute(idUser);
        }

        public async Task<List<User>> GetUsers()
        {
            return await spGetUsers.Execute();
        }

        public async Task RemoveUser(int idUser)
        {
            await spDeleteUser.Execute(idUser);
        }

        public async Task UpdateUser(User user)
        {
            await spUpdateUser.Execute(user);
        }
    }
}


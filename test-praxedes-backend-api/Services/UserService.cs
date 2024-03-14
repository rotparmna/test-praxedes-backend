using test_praxedes_backend_api.Contracts;
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

        public UserService(ISpGetUserById spGetUserById,
            ISpGetUsers spGetUsers,
            ISpInsertUser spInsertUser,
            ISpDeleteUser spDeleteUser,
            ISpUpdateUser spUpdateUser)
		{
            this.spGetUserById = spGetUserById;
            this.spGetUsers = spGetUsers;
            this.spInsertUser = spInsertUser;
            this.spDeleteUser = spDeleteUser;
            this.spUpdateUser = spUpdateUser;
        }

        public async Task CreateUser(User user)
        {
            await spInsertUser.Execute(user);
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


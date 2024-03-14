using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface IUserService
	{
		Task<List<User>> GetUsers();
        Task<User> GetUserById(int idUser);
		Task CreateUser(User user);
		Task RemoveUser(int idUser);
        Task UpdateUser(User user);
    }
}


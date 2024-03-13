using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface IFamilyGroupService
	{
		Task<List<FamilyGroup>> GetFamilyGroups(int idUser);
	}
}


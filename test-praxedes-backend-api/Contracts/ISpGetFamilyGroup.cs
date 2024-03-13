using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
    public interface ISpGetFamilyGroup
    {
        Task<List<SpGetFamilyGroup>> Execute(int userId);
    }
}
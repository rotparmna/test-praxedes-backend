using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
    public interface ISpGetFamilyGroup : IExecuteStoreProcedure<Task<List<SpGetFamilyGroup>>, int>
    {
    }
}
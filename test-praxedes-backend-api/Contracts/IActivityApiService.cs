using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface IActivityApiService
	{
		Task Create(ActivityApi activityApi);
        Task Update(ActivityApi activityApi);
    }
}


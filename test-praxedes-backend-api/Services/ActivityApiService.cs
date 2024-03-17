using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
    public class ActivityApiService : IActivityApiService
	{
        private readonly ISpInsertActivityApi spInsertActivityApi;
        private readonly ISpUpdateActivityApi spUpdateActivityApi;

        public ActivityApiService(ISpInsertActivityApi spInsertActivityApi,
            ISpUpdateActivityApi spUpdateActivityApi) 
		{
            this.spInsertActivityApi = spInsertActivityApi;
            this.spUpdateActivityApi = spUpdateActivityApi;

        }

        public async Task Create(ActivityApi activityApi)
        {
            await spInsertActivityApi.Execute(activityApi);
        }

        public async Task Update(ActivityApi activityApi)
        {
            await spUpdateActivityApi.Execute(activityApi);
        }
    }
}


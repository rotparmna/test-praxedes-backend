using Microsoft.AspNetCore.Mvc.Filters;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Filters
{
	public class LogFilter : IResultFilter
    {
        private readonly IActivityApiService activityApiService;

        public LogFilter(IActivityApiService activityApiService)
        {
            this.activityApiService = activityApiService;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            var activityApi = new ActivityApi()
            {
                IdActivityApi = context.HttpContext.TraceIdentifier,
                HttpStatusCode = context.HttpContext.Response.StatusCode.ToString()
            };
            if (context.Exception != null)
                activityApi.Exception = context.Exception.Message;
            activityApiService.Update(activityApi).Wait();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var activityApi = new ActivityApi()
            {
                IdActivityApi = context.HttpContext.TraceIdentifier,
                Resource = context.HttpContext.Request.PathBase,
                Path = context.HttpContext.Request.Path,
                Method = context.HttpContext.Request.Method
            };
            activityApiService.Create(activityApi).Wait();
        }
    }
}


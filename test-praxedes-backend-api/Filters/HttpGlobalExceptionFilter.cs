using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Dto;
using test_praxedes_backend_api.Exceptions;

namespace test_praxedes_backend_api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;
        private readonly IActivityApiService activityApiService;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env,
            ILogger<HttpGlobalExceptionFilter> logger,
            IActivityApiService activityApiService)
        {
            this.activityApiService = activityApiService;
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(ValidatorException))
            {
                ManageValidatorException(context);
            }
            else
            {
                ManageInternalServerError(context);
            }

            CreateActivity(context);
        }

        private void CreateActivity(ExceptionContext context)
        {
            activityApiService.Create(new Models.ActivityApi()
            {
                IdActivityApi = context.HttpContext.TraceIdentifier,
                Resource = context.HttpContext.Request.PathBase,
                Path = context.HttpContext.Request.Path,
                Method = context.HttpContext.Request.Method,
                Exception = context.Exception.Message,
                HttpStatusCode = ((int)HttpStatusCode.InternalServerError).ToString()
            }).Wait();
        }

        private void ManageInternalServerError(ExceptionContext context)
        {
            var json = new JsonErrorResponse
            {
                Messages = new[] { "An error ocurred." }
            };

            if (env.IsDevelopment())
                json.DeveloperMessage = context.Exception.Message;

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        private static void ManageValidatorException(ExceptionContext context)
        {
            var problemDetails = new ValidationProblemDetails()
            {
                Type = "Data",
                Detail = context.Exception.Message,
                Title = null
            };

            problemDetails.Errors.Add("Validations",
                ((IDataErrorException)context.Exception).DataError.ToArray());

            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}


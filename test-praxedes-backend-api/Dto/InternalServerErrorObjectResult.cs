using Microsoft.AspNetCore.Mvc;

namespace test_praxedes_backend_api.Dto
{
	public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}


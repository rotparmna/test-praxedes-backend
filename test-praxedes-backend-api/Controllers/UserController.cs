using Microsoft.AspNetCore.Mvc;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Dto;

namespace test_praxedes_backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IResult> GetUsers()
        {
            return Results.Ok(await userService.GetUsers());
        }

        [HttpGet]
        public async Task<IResult> GetUserById(int? idUser)
        {
            if (!idUser.HasValue)
                return Results.BadRequest();
            return Results.Ok(await userService.GetUserById(idUser.Value));
        }

        [HttpPost]
        public async Task<IResult> CreateUser([FromForm]UserDto user)
        {
            await userService.CreateUser(new()
            {
                Names = user.Names,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Gender = user.Gender,
                DocumentNumber = user.DocumentNumber
            });

            return Results.Ok();
        }

        [HttpDelete]
        public async Task<IResult> DeleteUser(int? idUser)
        {
            if (!idUser.HasValue)
                return Results.BadRequest();
            await userService.RemoveUser(idUser.Value);
            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> UpdateUser([FromForm]UserDto user)
        {
            await userService.UpdateUser(new()
            {
                Names = user.Names,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Gender = user.Gender,
                DocumentNumber = user.DocumentNumber,
                UserId = user.UserId
            });
            return Results.Ok();
        }
    }
}


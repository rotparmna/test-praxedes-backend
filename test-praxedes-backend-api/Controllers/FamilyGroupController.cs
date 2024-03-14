using Microsoft.AspNetCore.Mvc;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Dto;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test_praxedes_backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyGroupController : ControllerBase
    {
        private readonly IFamilyGroupService familyGroupService;

        public FamilyGroupController(IFamilyGroupService familyGroupService)
        {
            this.familyGroupService = familyGroupService;
        }

        [HttpGet]
        [Route("getByParent")]
        public async Task<IResult> GetFamilyGroups(int? idUser)
        {
            if (!idUser.HasValue)
                return Results.BadRequest();

            var familyGroupDto = new List<GetFamilyGroupDto>();
            var familyGroup = await this.familyGroupService.GetFamilyGroups(idUser.Value);
            familyGroup.ForEach(x =>
            {
                familyGroupDto.Add(new GetFamilyGroupDto()
                {
                    UserId = x.UserChild.UserId,
                    DocumentNumber = x.UserChild.DocumentNumber,
                    Names = x.UserChild.Names,
                    LastName = x.UserChild.LastName,
                    Gender = x.UserChild.Gender,
                    Relationship = x.Relationship,
                    Age = x.UserChild.Age,
                    Birthdate = x.UserChild.Birthdate
                });
            });

            return Results.Ok(familyGroupDto);
        }

        [HttpPost]
        [Route("addByParent")]
        public async Task<IResult> SaveFamilyGroup([FromQuery] int? idUserParent, [FromForm]AddFamilyGroupDto familyGroup)
        {
            if (!idUserParent.HasValue)
                return Results.BadRequest();

            await familyGroupService.Add(idUserParent.Value,
                new Models.FamilyGroup()
                {
                    Relationship = familyGroup.Relationship,
                    UserParent = new()
                    {
                        UserId=idUserParent.Value
                    },
                    UserChild = new()
                    {
                        UserId = familyGroup.User.UserId,
                        Names = familyGroup.User.Names,
                        LastName = familyGroup.User.LastName,
                        Gender = familyGroup.User.Gender,
                        Birthdate = familyGroup.User.Birthdate,
                        DocumentNumber = familyGroup.User.DocumentNumber
                    }
                });

            return Results.Ok();
        }
    }
}


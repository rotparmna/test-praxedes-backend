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
        public async Task<IResult> GetFamilyGroups(int? idUser)
        {
            if (!idUser.HasValue)
                return Results.BadRequest();

            var familyGroupDto = new List<FamilyGroupDto>();
            var familyGroup = await this.familyGroupService.GetFamilyGroups(idUser.Value);
            familyGroup.ForEach(x =>
            {
                familyGroupDto.Add(new FamilyGroupDto()
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
    }
}


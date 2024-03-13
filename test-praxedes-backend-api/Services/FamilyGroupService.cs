using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
	public class FamilyGroupService: IFamilyGroupService
    {
        private readonly ISpGetFamilyGroup spGetFamilyGroup;

        public FamilyGroupService(ISpGetFamilyGroup spGetFamilyGroup)
		{
            this.spGetFamilyGroup = spGetFamilyGroup;
        }

        public async Task<List<FamilyGroup>> GetFamilyGroups(int idUser)
        {
            var familyGroupDomain = new List<FamilyGroup>();
            var familyGroup = await spGetFamilyGroup.Execute(idUser);
            familyGroup.ForEach(x =>
            {
                familyGroupDomain.Add(new FamilyGroup()
                {
                    UserChild = new User()
                    {
                        UserId = x.UserId,
                        DocumentNumber=x.DocumentNumber,
                        Names=x.Names,
                        LastName = x.LastName,
                        Gender = x.Gender,
                        Birthdate = x.Birthdate
                    },
                    Relationship = x.Relationship
                });
            });

            return familyGroupDomain;
        }
    }
}


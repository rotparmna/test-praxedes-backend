using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Services
{
	public class FamilyGroupService: IFamilyGroupService
    {
        private readonly ISpGetFamilyGroup spGetFamilyGroup;
        private readonly ISpGetUser spGetUser;
        private readonly ISpInsertUserRelationship spInsertUserRelationship;
        private readonly ISpInsertUser spInsertUser;

        public FamilyGroupService(ISpGetFamilyGroup spGetFamilyGroup,
            ISpGetUser spGetUser,
            ISpInsertUserRelationship spInsertUserRelationship,
            ISpInsertUser spInsertUser)
		{
            this.spGetFamilyGroup = spGetFamilyGroup;
            this.spGetUser = spGetUser;
            this.spInsertUserRelationship = spInsertUserRelationship;
            this.spInsertUser = spInsertUser;
        }

        public async Task Add(int idUserParent, FamilyGroup familyGroup)
        {
            User userChild = await spGetUser.Execute(familyGroup.UserChild.DocumentNumber);
            if (!ExistUser(userChild))
                userChild.UserId = await CreateUser(familyGroup.UserChild);
            
            familyGroup.UserChild = userChild;
            await CreateRelationship(familyGroup);
        }

        public async Task<List<FamilyGroup>> GetFamilyGroups(int idUserParent)
        {
            var familyGroupDomain = new List<FamilyGroup>();
            var familyGroup = await spGetFamilyGroup.Execute(idUserParent);
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

        private bool ExistUser(User user)
        {
            return user.UserId != -1;
        }

        private async Task<int> CreateUser(User user)
        {
            return await spInsertUser.Execute(user);
        }

        private async Task CreateRelationship(FamilyGroup familyGroup)
        {
            await spInsertUserRelationship.Execute(familyGroup);
        }
    }
}


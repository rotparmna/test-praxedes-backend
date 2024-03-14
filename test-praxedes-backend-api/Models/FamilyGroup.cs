using System;
namespace test_praxedes_backend_api.Models
{
	public class FamilyGroup
	{
        public User UserChild { get; set; } = new User();
        public string Relationship { get; set; } = string.Empty;
        public User UserParent { get; set; } = new User();
    }
}


using System;
namespace test_praxedes_backend_api.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string DocumentNumber { get; set; }
		public string Names { get; set; }
		public string LastName { get; set; }
		public string? Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age
		{
			get
			{
				return (DateTime.Today - Birthdate).Days / 365;
			}
		}
		public bool IsMinor {
			get
			{
				return Age < 18;
			}
		}

		public List<FamilyGroup>? FamilyGroup { get; set; }
		
	}
}


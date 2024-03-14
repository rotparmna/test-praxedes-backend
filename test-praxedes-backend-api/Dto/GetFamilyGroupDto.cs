namespace test_praxedes_backend_api.Dto
{
	public class GetFamilyGroupDto
	{
        public int UserId { get; set; }
        public string DocumentNumber { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Relationship { get; set; }
        public int Age
        {
            get;set;
        }
        public bool IsMinor
        {
            get; set;
        }
    }
}


namespace API.Dtos
{
    public class UserDto
    {
        public DateTime CreationDate { get; set; }

        public string Role { get; set; } = string.Empty;

        public string Hash { get; set; } = string.Empty;

        public bool Active { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string JobType { get; set; } = string.Empty;

        public int IncomeLevel { get; set; }

        public string IdType { get; set; } = string.Empty;

        public string IdNumber { get; set; } = string.Empty;
    }
}

namespace API.Dtos.User
{
    public class OnUserCreationDto
    {
        public int Id { get; set; } 
        public DateTime? CreationDate { get; set; }

        public GetUserDto? Data { get; set; }
    }
}

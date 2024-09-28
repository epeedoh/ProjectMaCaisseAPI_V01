namespace ProjectMaCaisseAPI_V01.Models
{
    public class UserResponse : ApiReponse
    {
        public List<UserDto> users { get; set; }
        public bool isSaved { get; set; } = false;
        public bool userExist { get; set; } = false;
    }
}

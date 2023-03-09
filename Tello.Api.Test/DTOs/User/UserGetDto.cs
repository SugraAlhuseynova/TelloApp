namespace Tello.Api.Test.DTOs.User
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
        public List<string> RolesIds { get; set; }
    }
}

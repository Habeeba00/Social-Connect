namespace SocialConnect.DTOs.Profile
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}

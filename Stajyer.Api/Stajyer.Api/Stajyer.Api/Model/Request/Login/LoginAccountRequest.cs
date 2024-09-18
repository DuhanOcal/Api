namespace Stajyer.Api.Model.Request.Login
{
    public class LoginAccountRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string? UserType { get; set; }
    }
}

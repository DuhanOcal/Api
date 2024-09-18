namespace Stajyer.Api.Model.Response.Password
{
    public class ChangeUserPasswordResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Id { get; set; }
    }
}

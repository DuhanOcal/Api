namespace Stajyer.Api.Model.Response.Account
{
    public class LoginUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string? UserType { get; set; }
    }
}

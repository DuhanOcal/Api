namespace Stajyer.Api.Model.Request.Password
{
    public class ChangePasswordRequest
    {
        public Guid Id { get; set; }
        public string Password { get; set; }   
        public int UserType { get; set; }

    }
}

namespace Stajyer.Api.Model.Request.Customer
{
    public class AddCustomerRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}

namespace Stajyer.Api.Model.Request.Seller
{
    public class UpdateSellerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}

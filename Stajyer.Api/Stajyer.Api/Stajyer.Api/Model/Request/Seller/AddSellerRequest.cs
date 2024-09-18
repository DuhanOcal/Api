namespace Stajyer.Api.Model.Request.Seller
{
    public class AddSellerRequest
    {
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}

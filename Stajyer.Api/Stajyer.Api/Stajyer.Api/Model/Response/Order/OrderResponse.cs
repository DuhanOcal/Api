namespace Stajyer.Api.Model.Response.Order
{
    public class OrderResponse
    {
        public string ProductName { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public Guid AdressId { get; set; }
        public Guid CasrgoId { get; set; }
        public int Stock { get; set; }

    }
}

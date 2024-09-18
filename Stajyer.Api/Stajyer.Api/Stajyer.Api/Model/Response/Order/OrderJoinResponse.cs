namespace Stajyer.Api.Model.Response.Order
{
    public class OrderJoinResponse
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string AdressName { get; set; }
        public string CargoCompanyName { get; set; }
        public decimal? Price { get; set; }
        public int MyProperty { get; set; }
    }
}

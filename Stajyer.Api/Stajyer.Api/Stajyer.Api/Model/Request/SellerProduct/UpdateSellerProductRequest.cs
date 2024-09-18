namespace Stajyer.Api.Model.Request.SellerProduct
{
    public class UpdateSellerProductRequest
    {
        public Guid Id { get; set; }
        public string Explanation { get; set; }
        public int Stock { get; set; }

    }
}

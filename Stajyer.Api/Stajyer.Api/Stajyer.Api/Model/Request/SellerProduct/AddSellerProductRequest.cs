namespace Stajyer.Api.Model.Request.SellerProduct
{
    public class AddSellerProductRequest
    {
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public string SeriesNumber { get; set; }
        public string Explanation { get; set; }
        public int Stock { get; set; }
        public string Product { get; set; }
    }
}

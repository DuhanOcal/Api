namespace Stajyer.Api.Model.Response.Product
{
    public class GetProductResponse
    {
        public string SeriesNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public decimal Weight { get; set; }
        public string ProductType { get; set; }
    }
}

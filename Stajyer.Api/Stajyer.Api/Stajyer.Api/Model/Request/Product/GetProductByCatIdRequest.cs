namespace Stajyer.Api.Model.Request.Product
{
    public class GetProductByCatIdRequest
    {
        public string SeriesNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public decimal Weight { get; set; }
        public string ProductType { get; set; }
    }
}

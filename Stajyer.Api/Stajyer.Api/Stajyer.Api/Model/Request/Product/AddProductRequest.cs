namespace Stajyer.Api.Model.Request.Product
{
    public class AddProductRequest
    {
        public string SeriesNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public int Weight { get; set; }
        public string ProductType { get; set; }
    }
}

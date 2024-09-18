namespace Stajyer.Api.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string SeriesNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Weight { get; set; }
        public string ProductType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Product()
        
        {
           Id = Guid.NewGuid();
           CreatedAt = DateTime.Now;   
        }
        

    }
}

namespace Stajyer.Api.Data.Entities
{
    public class SellerProduct
    {
        public Guid  Id { get; set; }
        public Guid  SellerId { get; set; }
        public Guid  ProductId { get; set; }
        public string Explanation { get; set; }
        public int  Stock { get; set; }
        public DateTime  CreatedAt { get; set; }
        public DateTime?  UpdatedAt { get; set; }
        public DateTime?  DeletedAt { get; set; }


        public SellerProduct()
        { 
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}

namespace Stajyer.Api.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid SellerProductId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal? Price { get; set; } 
        public decimal ProductQuantity { get; set; }
        public string? ProductType   { get; set; }
        public Guid AdressId { get; set; }
        public Guid CargoId  { get; set; }
        public decimal? CargoPrice { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}

        public Order() 
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }

}

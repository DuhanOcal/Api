namespace Stajyer.Api.Data.Entities
{
    public class Comments
    {
        public Guid Id { get; set; }
        public Guid SellerProductId { get; set; }
        public string Explanation { get; set; }
        public Guid CustomerId { get; set; }
        public string SellerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        public Comments()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

    }
}

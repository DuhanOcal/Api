namespace Stajyer.Api.Model.Request.Order
{
    public class OrderRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public Guid AdressId { get; set; }
        public Guid CargoId { get; set; }
        public Guid SellerProductId { get; set; }

    }
}

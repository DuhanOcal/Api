namespace Stajyer.Api.Model.Request.Comments
{
    public class UpdateCommentsRequest
    {
        public  Guid SellerProductId { get; set; }
        public string Explanation { get; set; }
        public string SellerName { get; set; }
    }
}

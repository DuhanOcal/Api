using Stajyer.Api.Data.Entities;

namespace Stajyer.Api.Controllers
{
    public class JoinResponse
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public Adress Adress { get; set; }
        public Cargo Cargo { get; set; }
    }
}

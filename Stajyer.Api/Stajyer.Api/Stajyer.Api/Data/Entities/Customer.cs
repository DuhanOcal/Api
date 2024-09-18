using Microsoft.AspNetCore.Http.HttpResults;

namespace Stajyer.Api.Data.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Customer()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }

   
}

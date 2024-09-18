namespace Stajyer.Api.Data.Entities
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public string CargoCompanyName { get; set; } 
        public int Price { get; set; } 
       public DateTime CreatedAt { get; set; }
       public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Cargo() 
        {
            Id = Guid.NewGuid();
             CreatedAt= DateTime.Now;

        }
    }
}

namespace Stajyer.Api.Data.Entities
{
    public class Adress
    {
        public Guid Id { get; set; }
        public string AdressName { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }



        public Adress() 
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        }
}

namespace Stajyer.Api.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Category()
        { 
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}

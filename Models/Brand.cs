namespace EverythingSucks.Models
{
    public class Brand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}

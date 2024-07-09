namespace EverythingSucks.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public virtual ICollection<ProductType>? ProductTypes { get; set; }
    }
}

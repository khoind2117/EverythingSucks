using static System.Net.Mime.MediaTypeNames;

namespace EverythingSucks.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Slug { get; set; }

        public Guid? ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }

        public virtual ICollection<ProductColor>? ProductColors { get; set; }

        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}

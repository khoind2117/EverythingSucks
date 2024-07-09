namespace EverythingSucks.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Url { get; set; }
        public bool IsPrimary { get; set; }

        public Guid? ProductColorId { get; set; }
        public virtual ProductColor? ProductColor { get; set; }
    }
}
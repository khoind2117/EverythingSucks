namespace EverythingSucks.Models
{
    public class Color
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string ColorCode { get; set; }

        public virtual ICollection<ProductColor>? ProductColors { get; set; }
    }
}

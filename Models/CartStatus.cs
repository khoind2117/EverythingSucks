namespace EverythingSucks.Models
{
    public class CartStatus
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public virtual ICollection<Cart>? Carts { get; set; }
    }
}
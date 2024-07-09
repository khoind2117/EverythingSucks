namespace EverythingSucks.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid OrderStatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
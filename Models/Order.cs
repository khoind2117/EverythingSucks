namespace EverythingSucks.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid? OrderStatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
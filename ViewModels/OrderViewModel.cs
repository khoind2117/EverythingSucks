using EverythingSucks.Models;

namespace EverythingSucks.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? OrderStatusId { get; set; }
        public virtual OrderStatus? OrderStatus { get; set; }
    }
}

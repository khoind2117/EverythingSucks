namespace EverythingSucks.Models
{
    public class Favorite
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime FavoriteAt { get; set; }

        public string UserId { get; set; }
        public virtual User? User { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
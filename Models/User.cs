using Microsoft.AspNetCore.Identity;

namespace EverythingSucks.Models
{
    public class User : IdentityUser
    {
        public Guid? CartId { get; set; } = Guid.NewGuid();
        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}
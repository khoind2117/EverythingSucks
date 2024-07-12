using EverythingSucks.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public Guid? CartId { get; set; } = Guid.NewGuid();
        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public virtual ICollection<Favorite>? Favorites { get; set; }

        public User(ApplicationDbContext context)
        {
            CartId = Guid.NewGuid();
            Cart = new Cart { Id = CartId.Value, UserId = this.Id, CartStatusId = GetCartStatusIdByName(context, "Empty") };
        }

        private Guid? GetCartStatusIdByName(ApplicationDbContext context, string statusName)
        {
            var cartStatus = context.CartStatuses.FirstOrDefault(cs => cs.Name == statusName);
            return cartStatus?.Id;
        }
    }
}
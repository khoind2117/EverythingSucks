using EverythingSucks.Data;
using EverythingSucks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region GetListByUserId
        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var favorites = await _context.Favorites.Where(f => f.UserId == userId).ToListAsync();

            // Check if any favorites were found
            if (favorites.Count() == 0)
            {
                return NotFound("No favorites found for the specified user ID.");
            }

            // Return the favorites to the view
            return View(favorites);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Create(string userId, Guid productId)
        {
            if (ModelState.IsValid)
            {
                Favorite favorite = new Favorite()
                {
                    UserId = userId,
                    ProductId = productId,
                    Id = Guid.NewGuid(),
                    FavoriteAt = DateTime.UtcNow,
                };
                _context.Add(favorite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Put
        public async Task<IActionResult> EditFavorite(string userId, Guid oldProductId,Guid newProductId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == oldProductId);

            // Check if a favorite was found
            if (favorite == null)
            {
                return NotFound($"No favorite found for user ID '{userId}' and old product ID '{oldProductId}'.");
            }

            // Update the favorite's ProductId
            favorite.ProductId = newProductId;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return a NoContent result to indicate successful update
            return NoContent(); // HTTP status code 204
        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(string userId, Guid productId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            // Check if a favorite was found
            if (favorite == null)
            {
                return NotFound($"No favorite found for user ID '{userId}' and product ID '{productId}'.");
            }

            // Delete the favorite
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            // Return a NoContent result to indicate successful deletion
            return NoContent(); // HTTP status code 204
        }
        #endregion
    }
}

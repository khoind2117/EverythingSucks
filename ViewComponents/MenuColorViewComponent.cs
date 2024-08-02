using EverythingSucks.Data;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.ViewComponents
{
    public class MenuColorViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuColorViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var colors = await _context.Colors.ToListAsync();

            var data = colors.Select(c => new MenuColorViewModel
            {
                ColorId = c.Id,
                ColorName = c.Name,
                ColorCode = c.ColorCode
            }).ToList();

            return View(data); // Default.cshtml
        }
    }
}

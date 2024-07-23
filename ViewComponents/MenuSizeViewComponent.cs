using EverythingSucks.Data;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.ViewComponents
{
    public class MenuSizeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuSizeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sizes = await _context.Sizes.ToListAsync();

            var data = sizes.Select(b => new MenuSizeViewModel
            {
                SizeId = b.Id,
                SizeName = b.Name,
            }).ToList();

            return View(data); // Default.cshtml
        }
    }
}

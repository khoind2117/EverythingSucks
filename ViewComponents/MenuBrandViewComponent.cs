using EverythingSucks.Data;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.ViewComponents
{
    public class MenuBrandViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuBrandViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _context.Brands.ToListAsync();

            var data = brands.Select(b => new MenuBrandViewModel
            {
                BrandId = b.Id,
                BrandName = b.Name,
            }).ToList();

            return View(data); // Default.cshtml
        }
    }
}

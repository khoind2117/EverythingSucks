using EverythingSucks.Data;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuCategoryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.Include(c => c.ProductTypes)
                                                        .ThenInclude(pt => pt.Products)
                                                        .ToListAsync();

            var data = categories.Select(c => new MenuCategoryViewModel
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                CategoryCount = c.ProductTypes.SelectMany(pt => pt.Products).Count(),
                ProductTypes = c.ProductTypes.Select(pt => new ProductTypeViewModel
                {
                    ProductTypeId = pt.Id,
                    ProductTypeName = pt.Name,
                    ProductTypeCount = pt.Products.Count()
                }).ToList()
            }).ToList();

            return View(data); // Default.cshtml
        }

    }
}

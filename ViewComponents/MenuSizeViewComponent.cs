using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.ViewComponents
{
    public class MenuSizeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly List<string> sizeOrder = new List<string> {"M", "L", "XL", "2XL", "3XL" };

        public MenuSizeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        private IEnumerable<Size> GetSortedSizes(IEnumerable<Size> sizes)
        {
            return sizes.OrderBy(size => sizeOrder.IndexOf(size.Name));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách kích cỡ từ cơ sở dữ liệu
            var sizes = await _context.Sizes.ToListAsync();

            // Sắp xếp theo thứ tự đã định
            var sortedSizes = GetSortedSizes(sizes);

            // Chuyển đổi sang ViewModel
            var data = sortedSizes.Select(b => new MenuSizeViewModel
            {
                SizeId = b.Id,
                SizeName = b.Name,
            }).ToList();

            // Trả về view component với dữ liệu đã sắp xếp
            return View(data);
        }
    }
}

using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Product/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateProductViewModel
            {
                AvailableBrands = await _context.Brands.ToListAsync(),
                AvailableCategories = await _context.Categories.Include(c => c.ProductTypes).ToListAsync(),
                AvailableColors = await _context.Colors.ToListAsync()
            }; 
            return View(model);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    BrandId = productVM.BrandId,
                    ProductTypeId = productVM.ProductTypeId,
                    ProductColors = productVM.SelectedColorIds.Select(colorId => new ProductColor
                    {
                        ColorId = colorId
                    }).ToList()
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            else
            {
                // Trả về view với ModelState không hợp lệ và dữ liệu đã nhập
                ModelState.AddModelError("", "Failed to create product");
                return View(productVM);
            }
        }
    }
}

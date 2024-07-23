using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.Services;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PhotoService _photoService;

        public ProductController(ApplicationDbContext context,
            PhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Product/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateProductViewModel
            {
                AvailableBrands = await _context.Brands.ToListAsync(),
                AvailableCategories = await _context.Categories
                                        .Include(c => c.ProductTypes)
                                        .ToListAsync(),
                AvailableColors = await _context.Colors.ToListAsync()
            };

            return View(viewModel);
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
                    ProductColors = await Task.WhenAll(productVM.ColorSelections.Select(async cs => new ProductColor
                    {
                        ColorId = cs.ColorId,
                        ProductImages = new List<ProductImage>
                        {
                            new ProductImage
                            {
                                Url = (await _photoService.AddPhotoAsync(cs.PrimaryImage, 800, 800)).SecureUrl.ToString(),
                                IsPrimary = true
                            }
                        }.Concat(await Task.WhenAll(cs.AdditionalImages.Select(async image => new ProductImage
                        {
                            Url = (await _photoService.AddPhotoAsync(image, 400, 400)).SecureUrl.ToString(),
                            IsPrimary = false
                        }))).ToList()
                    }))
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            else
            {
                productVM.AvailableBrands = await _context.Brands.ToListAsync();
                productVM.AvailableCategories = await _context.Categories
                                        .Include(c => c.ProductTypes)
                                        .ToListAsync();
                productVM.AvailableColors = await _context.Colors.ToListAsync();

                // Trả về view với ModelState không hợp lệ và dữ liệu đã nhập
                ModelState.AddModelError("", "Failed to create product");
                return View(productVM);
            }
        }
    }
}

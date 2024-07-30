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

        public async Task<IActionResult> Index(Guid? productTypeId)
        {
            var products = _context.Products.Include(p => p.ProductColors)
                                                .ThenInclude(pc => pc.Color)
                                            .Include(p => p.ProductColors)
                                                .ThenInclude(pc => pc.ProductImages)
                                            .AsQueryable();

            if (productTypeId.HasValue)
            {
                products = products.Where(p => p.ProductTypeId == productTypeId.Value);
            }

            var result = await products.Select(p => new ProductViewModel
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ProductPrice = p.Price,
                ProductColors = p.ProductColors.Select(pc => new ProductColorViewModel
                {
                    ColorId = pc.ColorId,
                    ColorName = pc.Color != null ? pc.Color.Name : null,
                    ColorCode = pc.Color.ColorCode,
                    ProductImages = pc.ProductImages.Select(pi => new ProductImageViewModel
                    {
                        ImageUrl = pi.Url,
                        IsPrimary = pi.IsPrimary
                    }).ToList()
                }).ToList()
            }).ToListAsync();

            return View(result);
        }

        public async Task<IActionResult> Search(string query)
        {
            var products = _context.Products.AsQueryable();

            if (query != null)
            {
                products = products.Where(p => p.Name.Contains(query));
            }

            var result = await products.Select(p => new ProductViewModel
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ProductPrice = p.Price,
                ProductColors = p.ProductColors.Select(pc => new ProductColorViewModel
                {
                    ColorId = pc.ColorId,
                    ColorName = pc.Color != null ? pc.Color.Name : null,
                    ColorCode = pc.Color.ColorCode,
                    ProductImages = pc.ProductImages.Select(pi => new ProductImageViewModel
                    {
                        ImageUrl = pi.Url,
                        IsPrimary = pi.IsPrimary
                    }).ToList()
                }).ToList()
            }).ToListAsync();

            return View(result);
        }

        // GET: Product/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateProductViewModel
            {
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

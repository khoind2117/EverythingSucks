using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.Services;
using EverythingSucks.ViewModels;

namespace EverythingSucks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PhotoService _photoService;

        public ProductsController(ApplicationDbContext context,
            PhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
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

        // POST: Products/Create
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
                            Url = (await _photoService.AddPhotoAsync(image, 800, 800)).SecureUrl.ToString(),
                            IsPrimary = false
                        }))).ToList()
                    }))
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
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

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !_context.Products.Any())
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // No need to set ViewData["ProductId"] here unless you specifically need it in your view
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,CreatedAt,UpdatedAt,IsDeleted,ProductTypeId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }


        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverythingSucks.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GetList
        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            return View(brands);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateBrandViewModel
            {
                AvailableProducts = await _context.Products.ToListAsync(),
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandViewModel brandVM)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand
                {
                    Name = brandVM.Name
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("", "Failed to create brand");
            }

            return View(brandVM);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            var model = new EditBrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                AvailableProducts = await _context.Products.ToListAsync()
            };

            return View(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(EditBrandViewModel brandVM)
        {
            if (ModelState.IsValid)
            {
                var brand = await _context.Brands.FindAsync(brandVM.Id);
                if (brand == null)
                {
                    return NotFound();
                }

                brand.Name = brandVM.Name;

                _context.Brands.Update(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(brandVM);
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}

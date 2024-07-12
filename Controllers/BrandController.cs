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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            return View(brands);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
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
    }
}

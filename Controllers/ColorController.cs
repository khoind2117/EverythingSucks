using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace EverythingSucks.Controllers
{
    public class ColorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Color> colors = await _context.Colors.ToListAsync();
            return View(colors);
        }

        // GET: Color/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateColorViewModel colorVM)
        {
            if (ModelState.IsValid)
            {
                var color = new Color
                {
                    Name = colorVM.Name,
                    ColorCode = colorVM.ColorCode,
                };

                _context.Add(color);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to create color");
            }

            return View(colorVM);
        }
    }
}

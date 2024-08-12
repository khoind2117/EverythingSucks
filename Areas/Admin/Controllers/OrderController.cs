using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EverythingSucks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrderController(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderStatus)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            var orderHistory = orders.Select(order => new OrderViewModel
            {
                OrderId = order.Id,
                PaymentMethod = order.PaymentMethod,
                CreatedAt = order.CreatedAt,
                OrderStatusId = order.OrderStatusId,
                OrderStatus = order.OrderStatus
            }).ToList();

            return View(orderHistory);
        }

        [Authorize]
        public async Task<IActionResult> Detail(Guid orderId)
        {
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Size)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.ProductColor)
                            .ThenInclude(pc => pc.Product)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.ProductColor)
                            .ThenInclude(pc => pc.ProductImages)
                    .Where(o => o.Id == orderId)
                    .FirstOrDefaultAsync();

                if (order == null)
                {
                    return Redirect("/404");
                }

                var orderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    Id = oi.Id,
                    Quantity = oi.Quantity,
                    SizeId = oi.SizeId,
                    Size = oi.Size,
                    ProductColorId = oi.ProductColorId,
                    ProductColor = oi.ProductColor,
                    OrderId = oi.OrderId,
                    Order = oi.Order
                }).ToList();

                return View(orderItems);
            }
        }
    }
}

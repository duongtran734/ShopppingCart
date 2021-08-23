using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ShopppingCartDbContext _context;

        public ProductsController(ShopppingCartDbContext context)
        {
            this._context = context;
        }

        //GET /admin/products/index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(p=>p.Category).ToListAsync()) ;
        }
    }
}

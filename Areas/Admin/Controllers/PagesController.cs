using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopppingCart.Data;
using ShopppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly ShopppingCartDbContext _context;

        public PagesController(ShopppingCartDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pages = _context.Pages.OrderBy(p => p.Sorting);
            List<Page> pagelist = await pages.AsNoTracking().ToListAsync();


            return View(pagelist);
        }
    }
}

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

        //GET /admin/pages/[index]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pages = _context.Pages.OrderBy(p => p.Sorting);
            List<Page> pagelist = await pages.AsNoTracking().ToListAsync();
            return View(pagelist);
        }

        //GET /admin/pages/details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //get a specific page
            Page page = await _context.Pages.FirstOrDefaultAsync(p => p.Id == id);
            if(page == null)
            {
                return NotFound();
            }
            return View(page);
        }
    }
}

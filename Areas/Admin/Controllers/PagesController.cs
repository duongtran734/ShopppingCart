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
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        //GET /admin/pages/ccreate
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        //POST /admin/pages/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                //check if slug is existed
                var slug = await _context.Pages.FirstOrDefaultAsync(p => p.Slug == page.Slug);
                if (slug != null)
                {
                    //this error will be displayed in asp-validation-summary
                    ModelState.AddModelError("", "The title already exsits.");
                    return View(page);
                }
                //if not add to DB
                _context.Add(page);
                await _context.SaveChangesAsync();

                // since its Redirect, we have to use TempData
                //Notification message when successfully added, this is used with partial view
                TempData["Success"] = "The page has been added! ";
                return RedirectToAction("index");
            }

            return View(page);
        }

        //GET /admin/pages/edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //get a specific page
            // can use FindAsync instead of FirstOrDefaultAsync
            Page page = await _context.Pages.FirstOrDefaultAsync(p => p.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        //POST /admin/pages/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                //check if slug is existed
                var slug = await _context.Pages
                    .Where(x => x.Id != page.Id) // this will make sure to check others slug thats not the one we are editing
                    .FirstOrDefaultAsync(p => p.Slug == page.Slug);
                if (slug != null)
                {
                    //this error will be displayed in asp-validation-summary
                    ModelState.AddModelError("", "The page already exsits.");
                    return View(page);
                }
                //if not add to DB
                _context.Update(page);
                await _context.SaveChangesAsync();

                // since its Redirect, we have to use TempData
                //Notification message when successfully added, this is used with partial view
                TempData["Success"] = "The page has been edited! ";

                //redirection the edit page with id parameter
                return RedirectToAction("Edit", new { id = page.Id });
            }

            return View(page);
        }

        //GET /admin/pages/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //get a specific page
            // can use FindAsync instead of FirstOrDefaultAsync
            Page page = await _context.Pages.FirstOrDefaultAsync(p => p.Id == id);
            if (page == null)
            {
                TempData["Error"] = "The page does not exist!";
                return NotFound();
            }
            else
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The page has been deleted!";
            }
            return RedirectToAction("Index");
        }

    }

}

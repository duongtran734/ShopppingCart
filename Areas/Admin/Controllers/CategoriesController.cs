using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class CategoriesController : Controller
    {
        private readonly ShopppingCartDbContext _context;

        public CategoriesController(ShopppingCartDbContext context)
        {
            this._context = context;
        
        }

        //GET admin/categories/index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.OrderBy(c => c.Sorting).ToListAsync();
            return View(categories);
        }

        //GET admin/categories/create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post admin/categories/create
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                var slug = await _context.Categories.FirstOrDefaultAsync(s => s.Slug == category.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("","The category already exist!");
                    return View(category);
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The category has been added!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET admin/categories/edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST admin/categories/edit
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                //check if slug is existed
                var slug = await _context.Categories
                    .Where(x => x.Id != category.Id) // this will make sure to check others slug thats not the one we are editing
                    .FirstOrDefaultAsync(p => p.Slug == category.Slug);
                if (slug != null)
                {
                    //this error will be displayed in asp-validation-summary
                    ModelState.AddModelError("", "The category already exsits.");
                    return View(category);
                }
                //if not add to DB
                _context.Update(category);
                await _context.SaveChangesAsync();

                // since its Redirect, we have to use TempData
                //Notification message when successfully added, this is used with partial view
                TempData["Success"] = "The category has been edited! ";

                //redirection the edit page with id parameter
                return RedirectToAction("Edit", new { id = category.Id });
            }

            return View(category);
        }

        //GET /admin/categories/delete/5
        //We can also use POST request for delete, which is what the scaffold code does
        [HttpGet] 
        public async Task<IActionResult> Delete(int id)
        {
            //get a specific page
            // can use FindAsync instead of FirstOrDefaultAsync
            Category category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (category == null)
            {
                TempData["Error"] = "The category does not exist!";
            }
            else
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The category has been deleted!";
            }
            return RedirectToAction("Index");
        }


        //POST /admin/categories/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            //NOTICE: the param name cant be same as the posting data name (ids(post) and id(param))
            // id[] will arrive in the order it was sorted
            int count = 1;

            foreach (var categoryId in id)
            {
                Category category = await _context.Categories.FindAsync(categoryId);
                category.Sorting = count;
                _context.Update(category);
                await _context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}

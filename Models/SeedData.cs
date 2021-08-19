using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopppingCart.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ////The compiler will dispose the object when done with using() statement
            using (var context = new ShopppingCartDbContext
                (serviceProvider.GetRequiredService<DbContextOptions<ShopppingCartDbContext>>()))
            {
                //check of DB is empty
                if (context.Pages.Any())
                {
                    return;
                }

                //Seed data
                context.Pages.AddRange(
                    new Page
                    {
                        Title="Home",
                        Slug="home",
                        Content="home page",
                        Sorting =0
                    }, 
                    new Page
                    {
                        Title = "About Us",
                        Slug = "about-us",
                        Content = "about page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "Services",
                        Slug = "services",
                        Content = "services page",
                        Sorting = 100
                    }, 
                    new Page
                    {
                        Title = "Contact",
                        Slug = "contact",
                        Content = "contact page",
                        Sorting = 100
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}

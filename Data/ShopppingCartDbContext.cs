using Microsoft.EntityFrameworkCore;
using ShopppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopppingCart.Data
{
    public class ShopppingCartDbContext : DbContext
    {
        //Constructor
        public ShopppingCartDbContext(DbContextOptions<ShopppingCartDbContext> options) : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
    }
}

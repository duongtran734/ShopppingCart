using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopppingCart.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required,MinLength(4,ErrorMessage ="Minimum length is 2")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string image { get; set; }


        //////////////////////////////////////////
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
                Count = 1;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Products Products { get; set; }

        [Range(1, 100, ErrorMessage = "Select a count between 1 and 100")]
        public int Count { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey("AppId")]
        [ValidateNever]
        public AppUser AppUser  { get; set; }
    }
}

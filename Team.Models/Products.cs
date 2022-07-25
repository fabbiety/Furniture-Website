using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Models
{
    public class Products
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }

        public string Image { get; set; }
        [Range(1,1000, ErrorMessage = "Price should be between 1GBP and 1500GBP")]
        public double Price { get; set; }
        [Display(Name ="Furniture Type")]
        public int FurnitureTypeId { get; set; }
        [ForeignKey("FurnitureTypeId")]
        public FurnitureTypes FurnitureType { get; set;}
        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }
        public Category category { get; set; }


    }
}

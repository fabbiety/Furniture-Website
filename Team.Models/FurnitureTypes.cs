using System.ComponentModel.DataAnnotations;

namespace Team.Models
{
    public class FurnitureTypes
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
        
       
    }
}

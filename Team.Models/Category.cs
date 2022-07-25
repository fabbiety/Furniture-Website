using System.ComponentModel.DataAnnotations;

namespace Team.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
        [Range(1, 100, ErrorMessage ="Display order must be between 1-100")]
        public int Orders { get; set; }
    }
}

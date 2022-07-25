using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Message { get; set; }

        [Display(Name = "Phone Number")]
        public double ContactNumber { get; set; }

        [StringLength(25)]
        public string Email { get; set; }



    }
}

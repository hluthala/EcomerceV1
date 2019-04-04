using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kissi.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "City")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Company> Companies { get; set; }

    }
}
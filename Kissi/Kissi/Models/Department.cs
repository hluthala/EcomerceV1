using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kissi.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50,ErrorMessage ="The field {0} must be at least {1} chracteres")]
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }

    }
}
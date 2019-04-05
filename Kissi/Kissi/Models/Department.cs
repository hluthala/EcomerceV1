using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kissi.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50,ErrorMessage ="The field {0} must be at least {1} chracteres")]
        [Index("Department_Name_Index", IsUnique = true)]
        [Display(Name ="Department")]
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}
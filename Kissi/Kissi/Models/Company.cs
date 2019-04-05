using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Kissi.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "Company")]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(20, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Address { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        public int CityId { get; set; }
        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}
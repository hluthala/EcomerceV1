using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Kissi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
      
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(256, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Index("User_UserName_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(20, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Address { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Display(Name = "User")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
    }
}
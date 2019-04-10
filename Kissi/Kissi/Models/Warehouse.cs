using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kissi.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        [Index("Warehouse_CompanyId_Name_Index", 1, IsUnique = true)]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(256, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Index("Warehouse_CompanyId_Name_Index",2, IsUnique = true)]
        [Display(Name = "Warehouse")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(20, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Address { get; set; }
      
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can be empty")]
        [Display(Name = "City")]
        public int CityId { get; set; }
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kissi.Models
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "Tax")]
        [Index("Tax_CompanyId_Description_Index", 2, IsUnique = true)]
        public string Description { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, 1, ErrorMessage = "you must select {0} between {1} and {2}")]
        public double Rate { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can't be empty select  ")]
        [Index("Tax_CompanyId_Description_Index", 1, IsUnique = true)]
        [Display(Name = "Department")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
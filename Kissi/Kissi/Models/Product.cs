using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kissi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can't be empty select  ")]
        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]
        [Index("Product_CompanyId_BarCode_Index", 1, IsUnique = true)]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "Tax")]
        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        public string Description { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "Bar Code")]
        [Index("Product_CompanyId_BarCode_Index", 2, IsUnique = true)]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can't be empty select  ")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "The field {0} can't be empty select  ")]
        [Display(Name = "Tax")]
        public int TaxId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "you must select {0} between {1} and {2}")]
        public decimal Price { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [MaxLength(20, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        [Display(Name = "Dose")]
        public string Dose { get; set; }
        [DataType(DataType.MultilineText)]
        public string Posologie { get; set; }
        [DataType(DataType.MultilineText)]
        public string Indication { get; set; }
        public bool Ordonance { get; set; }
        [Display(Name = "Forme pharmaceutiques")]
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string FormePharma { get; set; }
        [Display(Name = "Voie d'admission")]
        [MaxLength(20, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Voieadmis { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Conditionnement { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Types { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Fabricant { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} must be at least {1} chracteres")]
        public string Origine { get; set; }
        [Display(Name = "Contre indication")]
        [DataType(DataType.MultilineText)]
        public string ContreIndication { get; set; }
        [Display(Name = "Effet sécondaire")]
        [DataType(DataType.MultilineText)]
        public string EffetSecond { get; set; }
        public virtual Company Company { get; set; }
        public virtual Category Category { get; set; }
        public virtual Tax Tax { get; set; }
    }
}
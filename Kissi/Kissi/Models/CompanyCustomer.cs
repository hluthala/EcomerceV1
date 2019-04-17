using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kissi.Models
{
    public class CompanyCustomer
    {
        public int CompanyCustomerId { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
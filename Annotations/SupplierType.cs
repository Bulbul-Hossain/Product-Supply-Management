using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductSupplyManagement.Models
{
    [MetadataType(typeof(SupplierType))]
    public partial class Supplier
    {
    }
    public class SupplierType
    {
        public int SupplierId { get; set; }
        [Required, StringLength(50), Display(Name ="Company Name")]
        public string SupplierCompany { get; set; }
        [Required, StringLength(20), Display(Name = "Contact No")]
        public string ContactNo { get; set; }
        [Required, StringLength(150), Display(Name = "Company Address")]
        public string Address { get; set; }
        [Required, StringLength(50),EmailAddress, Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
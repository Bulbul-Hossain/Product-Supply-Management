using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductSupplyManagement.Models
{
    [MetadataType(typeof(ProductType))]
    public partial class Product
    {

    }
    public partial class ProductType
    {
        
        public int ProductId { get; set; }
        [Required, StringLength(50), Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Required, DisplayFormat(DataFormatString ="{0:0.00}", ApplyFormatInEditMode =true)]
        public decimal UnitPrice { get; set; }
        [Required, StringLength(20)]
        public string SellUnit { get; set; }

        
    }
}
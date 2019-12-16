using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductSupplyManagement.Models
{
    [MetadataType(typeof(InventoryType))]
    public partial class Inventory
    {
        

        
    }
    public class InventoryType
    {
        public int InventoryId { get; set; }
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SupplierId { get; set; }
       
        public string LotNo { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductSupplyManagement.ViewModels
{
    public class SupplyViewModel
    {
        public int SupplierId { get; set; }
        [Display(Name = "Date"), DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }
        public int Quantity { get; set; }
    }
}
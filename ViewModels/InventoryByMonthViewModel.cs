using ProductSupplyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductSupplyManagement.ViewModels
{
    public class InventoryByMonthViewModel
    {
        public string MonthYear { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public IEnumerable<Inventory> Inventories { get; set; }
        public int TotalQ { get; set; }
    }
}
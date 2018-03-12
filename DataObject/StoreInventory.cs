using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataObject
{
    public class StoreInventory
    {
        //[Key]
        public int StoreID { get; set; }
        //[Key]
        public int ProductID { get; set; }
        public int StockLevel { get; set; }

        //expanded from product
        public string ProductName { get; set; }

        //expanded from store
        public string StoreName { get; set; }
    }
}

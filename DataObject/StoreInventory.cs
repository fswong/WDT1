using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public class StoreInventory
    {
        public long StoreID { get; set; }
        public long ProductID { get; set; }
        public long StockLevel { get; set; }

        //expanded from product
        public string ProductName { get; set; }

        //expanded from store
        public string StoreName { get; set; }
    }
}

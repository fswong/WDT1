using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public class OwnerInventory
    {
        //from original table
        public long ProductID { get; set; }
        public long StockLevel { get; set; }

        //expanded from Product table
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataObject
{
    public class OwnerInventory
    {
        //from original table
        [Key]
        public int ProductID { get; set; }
        public int StockLevel { get; set; }

        //expanded from Product table
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataObject
{
    public class StockRequest
    {
        [Key]
        public int StockRequestID { get; set; }
        public int StoreID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        //expanded from product
        public string ProductName { get; set; }

        //expanded from store
        public string StoreName { get; set; }

        //expanded from owner inventory
        public int CurrentStock { get; set; }
        public bool StockAvailability { get; set; }
    }
}

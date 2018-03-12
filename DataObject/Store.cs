using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataObject
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }
        public string Name { get; set; }
    }
}

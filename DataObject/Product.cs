using System;
using System.ComponentModel.DataAnnotations;

namespace DataObject
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
    }
}

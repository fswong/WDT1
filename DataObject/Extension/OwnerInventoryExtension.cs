using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataObject.Extension
{
    public static class OwnerInventoryExtension
    {
        /// <summary>
        /// converts a datatable to a list of owner inventory poco
        /// </summary>
        /// <param name="theDT"></param>
        /// <returns></returns>
        public static List<DataObject.OwnerInventory> ToOwnerInventoryListPOCO(this DataTable theDT)
        {
            try
            {
                List<DataObject.OwnerInventory> stores = new List<DataObject.OwnerInventory>();
                foreach (DataRow row in theDT.Rows)
                {
                    stores.Add(
                        new DataObject.OwnerInventory
                        {
                            ProductID = Convert.ToInt32(row["ProductID"]),
                            StockLevel = Convert.ToInt32(row["StockLevel"]),
                            Name = row["Name"].ToString()
                        }
                        );
                }
                return stores;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// converts string array to owner inventory poco
        /// </summary>
        /// <param name="theArray"></param>
        /// <returns></returns>
        public static DataObject.OwnerInventory ToOwnerInventoryPOCO(this string[] theArray) {
            try {
                return new DataObject.OwnerInventory
                {
                    ProductID = Convert.ToInt32(theArray[0]),
                    StockLevel = Convert.ToInt32(theArray[1]),
                    Name = theArray[2].ToString()
                };
            }
            catch (Exception) {
                throw;
            }
        }
    }
}

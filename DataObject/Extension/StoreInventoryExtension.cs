using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataObject.Extension
{
    public static class StoreInventoryExtension
    {
        /// <summary>
        /// converts a datatable to a list of store poco
        /// </summary>
        /// <param name="theDT"></param>
        /// <returns></returns>
        public static List<DataObject.StoreInventory> ToStoreInventoryListPOCO(this DataTable theDT)
        {
            try
            {
                List<DataObject.StoreInventory> result = new List<DataObject.StoreInventory>();
                foreach (DataRow row in theDT.Rows)
                {
                    result.Add(
                        new DataObject.StoreInventory
                        {
                            StoreID = Convert.ToInt32(row["StoreID"]),
                            ProductID = Convert.ToInt32(row["ProductID"]),
                            StockLevel = Convert.ToInt32(row["StockLevel"]),
                            ProductName = row["ProductName"].ToString(),
                            StoreName = row["StoreName"].ToString()
                        }
                        );
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// converts an array of strings to store poco
        /// </summary>
        /// <param name="theArray"></param>
        /// <returns></returns>
        public static DataObject.StoreInventory ToStoreInventoryPOCO(this string[] theArray)
        {
            try
            {
                return new DataObject.StoreInventory
                {
                    StoreID = Convert.ToInt32(theArray[0]),
                    ProductID = Convert.ToInt32(theArray[1]),
                    StockLevel = Convert.ToInt32(theArray[2]),
                    ProductName = theArray[3].ToString(),
                    StoreName = theArray[4].ToString()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

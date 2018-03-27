using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataObject.Extension
{
    public static class StockRequestExtension
    {
        /// <summary>
        /// converts a datatable to a list of store poco
        /// </summary>
        /// <param name="theDT"></param>
        /// <returns></returns>
        public static List<DataObject.StockRequest> ToStockRequestListPOCO(this DataTable theDT)
        {
            try
            {
                List<DataObject.StockRequest> result = new List<DataObject.StockRequest>();
                foreach (DataRow row in theDT.Rows)
                {
                    result.Add(
                        new DataObject.StockRequest
                        {
                            StockRequestID = Convert.ToInt32(row["StockRequestID"]),
                            StoreID = Convert.ToInt32(row["StoreID"]),
                            ProductID = Convert.ToInt32(row["ProductID"]),
                            Quantity = Convert.ToInt32(row["Quantity"]),
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
        public static DataObject.StockRequest ToStockRequestPOCO(this string[] theArray)
        {
            try
            {
                return new DataObject.StockRequest
                {
                    StockRequestID = Convert.ToInt32(theArray[0]),
                    StoreID = Convert.ToInt32(theArray[1]),
                    ProductID = Convert.ToInt32(theArray[2]),
                    Quantity = Convert.ToInt32(theArray[3]),
                    ProductName = theArray[4].ToString(),
                    StoreName = theArray[5].ToString()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

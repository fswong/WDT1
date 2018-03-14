using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataObject.Extension
{
    public static class StoreExtension
    {
        /// <summary>
        /// converts a datatable to a list of store poco
        /// </summary>
        /// <param name="theDT"></param>
        /// <returns></returns>
        public static List<DataObject.Store> ToStoreListPOCO(this DataTable theDT) {
            try {
                List<DataObject.Store> result = new List<DataObject.Store>();
                foreach (DataRow row in theDT.Rows) {
                    result.Add(
                        new DataObject.Store
                        {
                            StoreID = Convert.ToInt32(row["StoreID"]),
                            Name = row["Name"].ToString()
                        }
                        );
                }
                return result;
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// converts an array of strings to store poco
        /// </summary>
        /// <param name="theArray"></param>
        /// <returns></returns>
        public static DataObject.Store ToStorePOCO(this string[] theArray) {
            try {
                return new DataObject.Store {
                    StoreID = Convert.ToInt32(theArray[0]),
                    Name = theArray[1].ToString()
                };
            } catch (Exception) {
                throw;
            }
        }
    }
}

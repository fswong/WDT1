using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataObject.Extension
{
    public static class ProductExtension
    {
        /// <summary>
        /// converts a datatable to a list of product poco
        /// </summary>
        /// <param name="theDT"></param>
        /// <returns></returns>
        public static List<DataObject.Product> ToProductListPOCO(this DataTable theDT)
        {
            try
            {
                List<DataObject.Product> result = new List<DataObject.Product>();
                foreach (DataRow row in theDT.Rows)
                {
                    result.Add(
                        new DataObject.Product
                        {
                            ProductID = Convert.ToInt32(row["ProductID"]),
                            Name = row["Name"].ToString()
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
        /// converts an array of strings to product poco
        /// </summary>
        /// <param name="theArray"></param>
        /// <returns></returns>
        public static DataObject.Product ToProductPOCO(this string[] theArray)
        {
            try
            {
                return new DataObject.Product
                {
                    ProductID = Convert.ToInt32(theArray[0]),
                    Name = theArray[1].ToString()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

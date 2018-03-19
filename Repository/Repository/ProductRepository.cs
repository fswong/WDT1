using DataObject.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProductRepository
    {
        #region properties
        #endregion

        #region ctor
        public ProductRepository(){ }
        #endregion

        #region get
        /// <summary>
        /// generic get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject.Product GetProductById(int id) {
            try {
                string query = $" SELECT * FROM Product WHERE ProductID='{id}' ";
                return DBConn.Select(query).ToProductPOCO();
            }
            catch (Exception) {
                throw;
            }
        }

        public List<DataObject.Product> GetProductList() {
            try
            {
                string query = $" SELECT * FROM Product; ";
                return DBConn.GetDataTable(query).ToProductListPOCO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DataObject.Product> GetNotInInventory(int StoreID) {
            try
            {
                string query = $" SELECT p.* FROM Product p " +
                    $" WHERE ProductID NOT IN ( SELECT ProductID FROM StoreInventory WHERE StoreID={StoreID} ) ";
                return DBConn.GetDataTable(query).ToProductListPOCO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns list of products available/unavailable in store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="inInventory"></param>
        /// <returns></returns>
        //public List<DataObject.Product> GetInventoryProduct(int storeId, bool inInventory) {
        //    try
        //    {
        //        // TODO fix this
        //        string query = " SELECT * FROM Product WHERE Id ";
        //        if (inInventory) {
        //            query += " IN ";
        //        }
        //        else {
        //            query += " NOT IN ";
        //        }
        //        query += $" (SELECT DISTINCT ProductId FROM Inventory WHERE StoreId = '{storeId}') ";
        //        return DBConn.GetDataTable(query).To
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        
        #endregion

        #region patch

        #endregion
    }
}

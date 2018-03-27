using DataObject.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StoreInventoryRepository
    {
        #region ctor
        public StoreInventoryRepository() { }
        #endregion

        #region get
        /// <summary>
        /// lists all store inventory by store
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="inInventory"></param>
        /// <returns></returns>
        public List<DataObject.StoreInventory> GetStoreInventoryByStoreId(int StoreID, bool inInventory) {
            try
            {
                string query = " SELECT si.*, p.Name AS ProductName, s.Name AS StoreName FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductID = p.ProductID " +
                    " INNER JOIN Store s ON si.StoreID = s.StoreID " +
                    $" WHERE si.StoreID = '{StoreID}' ";
                return DBConn.GetDataTable(query).ToStoreInventoryListPOCO();
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// get selected store inventory item
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public DataObject.StoreInventory GetStoreInventoryByStoreIdAndProductId(int StoreID, int ProductID) {
            try
            {
                string query = " SELECT si.*, p.Name AS ProductName, s.Name AS StoreName FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductID = p.ProductID " +
                    " INNER JOIN Store s ON si.StoreID = s.StoreID " +
                    $" WHERE si.StoreID = '{StoreID}' AND si.ProductID = '{ProductID}' ";
                return DBConn.Select(query).ToStoreInventoryPOCO();
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// lists all products in a store that has quantity less than the threshold amount
        /// haha
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="Threshold"></param>
        /// <returns></returns>
        public List<DataObject.StoreInventory> GetStoreRequestByStoreIdAndThreshold(int StoreID, int Threshold) {
            try
            {
                string query = " SELECT si.*, p.Name AS ProductName, s.Name AS StoreName FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductID = p.ProductID " +
                    " INNER JOIN Store s ON si.StoreID = s.StoreID " +
                    $" WHERE si.StoreID = '{StoreID}' AND si.StockLevel < {Threshold} ";
                return DBConn.GetDataTable(query).ToStoreInventoryListPOCO();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region create
        /// <summary>
        /// generic insert method
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public DataObject.StoreInventory CreateStoreInventory(int StoreID, int ProductID, int Quantity = 1) {
            try {
                string query = " INSERT INTO StoreInventory " +
                    " (StoreID,ProductID,StockLevel) VALUES " +
                    $" ({StoreID},{ProductID},'{Quantity}') ";
                DBConn.Insert(query);

                return GetStoreInventoryByStoreIdAndProductId(StoreID, ProductID); ;
            } catch (Exception) {
                throw;
            }
        }
        #endregion

        #region patch
        /// <summary>
        /// Update the quantity 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="StoreID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public DataObject.StoreInventory UpdateStoreInventory(int ProductID, int StoreID, long Quantity) {
            try {
                string query = $" UPDATE StoreInventory SET StockLevel = '{Quantity}' WHERE ProductID = '{ProductID}' AND StoreID = '{StoreID}'; ";
                DBConn.Update(query);

                return GetStoreInventoryByStoreIdAndProductId(StoreID, ProductID); ;
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

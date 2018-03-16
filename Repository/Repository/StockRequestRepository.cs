using DataObject.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StockRequestRepository
    {
        #region ctor
        public StockRequestRepository() { }
        #endregion

        #region get
        /// <summary>
        /// lists all stockrequests
        /// </summary>
        /// <returns></returns>
        public List<DataObject.StockRequest> ListStockRequests() {
            try {
                string query = " SELECT sr.*, s.Name AS StoreName, p.Name AS ProductName, oi.StockLevel AS CurrentStock, " +
                    " CAST(CASE WHEN sr.Quantity >= StockLevel THEN 1 ELSE 0 END AS BIT) AS StockAvailability " +
                    " FROM StockRequest sr " + 
                    " INNER JOIN Store s ON sr.StoreID = s.StoreID " +
                    " INNER JOIN Product p ON sr.ProductID = p.ProductID " +
                    " INNER JOIN OwnerInventory oi ON sr.ProductID = oi.ProductID ";
                return DBConn.GetDataTable(query).ToStockRequestListPOCO();
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// returns single stock request
        /// </summary>
        /// <param name="StockRequestID"></param>
        /// <returns></returns>
        public DataObject.StockRequest GetStockRequestById(int StockRequestID) {
            try {
                string query = " SELECT sr.*, s.Name AS StoreName, p.Name AS ProductName, oi.StockLevel AS CurrentStock, " +
                    " CAST(CASE WHEN sr.Quantity >= StockLevel THEN 1 ELSE 0 END AS BIT) AS StockAvailability " +
                    " FROM StockRequest sr " +
                    " INNER JOIN Store s ON sr.StoreID = s.StoreID " +
                    " INNER JOIN Product p ON sr.ProductID = p.ProductID " +
                    " INNER JOIN OwnerInventory oi ON sr.ProductID = oi.ProductID " +
                    $" WHERE StockRequestID = '{StockRequestID}' ";
                    return DBConn.Select(query).ToStockRequestPOCO();
            } catch (Exception) {
                throw;
            }
        }
        #endregion

        #region delete
        /// <summary>
        /// generic row delete
        /// </summary>
        /// <param name="StockRequestID"></param>
        public void DeleteStockRequest(int StockRequestID)
        {
            try
            {
                string query = $" SELECT * FROM StockRequest WHERE StockRequestID = '{StockRequestID}' ";
                DBConn.Update(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

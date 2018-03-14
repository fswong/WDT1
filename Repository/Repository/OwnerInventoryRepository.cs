using DataObject.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class OwnerInventoryRepository
    {
        #region ctor
        public OwnerInventoryRepository(){ }
        #endregion

        #region get
        /// <summary>
        /// list all 
        /// </summary>
        /// <returns></returns>
        public List<DataObject.OwnerInventory> ListOwnerInventory() {
            try {
                string query = " SELECT oi.*, p.Name FROM " +
                    " OwnerInventory oi INNER JOIN " +
                    " Product p ON oi.ProductID = p.ProductID ";
                return DBConn.GetDataTable(query).ToOwnerInventoryListPOCO();
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="allowedNotFound"></param>
        /// <returns></returns>
        public DataObject.OwnerInventory GetOwnerInventoryById(int id) {
            try {
                string query = $" SELECT oi.*,p.Name FROM OwnerInventory oi " +
                    " INNER JOIN Product p ON oi.ProductID = p.ProductID " +
                    $" WHERE oi.ProductID = '{id}' ";
                return DBConn.Select(query).ToOwnerInventoryPOCO();
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion

        #region patch
        /// <summary>
        /// update quantity for a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject.OwnerInventory UpdateOwnerInventory(int ProductID, long Quantity){
            try {
                string query = $" UPDATE OwnerInventory SET StockLevel = '{Quantity}' WHERE ProductId = '{ProductID}' ";
                DBConn.Update(query);

                return GetOwnerInventoryById(ProductID);
            } catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StoreInventoryRepository : Repository
    {
        #region ctor
        public StoreInventoryRepository(UnitOfWork uow) : base(uow:uow) { }
        #endregion

        #region get
        /// <summary>
        /// lists all store inventory by store
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="inInventory"></param>
        /// <returns></returns>
        public List<DataObject.StoreInventory> GetStoreInventoryByStoreId(long StoreID, bool inInventory) {
            try
            {
                // TODO fix query
                string query = " SELECT si.* FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductId = p.ProductId ";
                return _context.StoreInventorySet.FromSql(query).ToList();
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
        /// <param name="allowedNotFound"></param>
        /// <returns></returns>
        public DataObject.StoreInventory GetStoreInventoryByStoreIdAndProductId(long StoreID, long ProductID, bool allowedNotFound = false) {
            try
            {
                // TODO fix query
                string query = " SELECT si.* FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductId = p.ProductId ";
                if (allowedNotFound) {
                    return _context.StoreInventorySet.FromSql(query).FirstOrDefault();
                }
                else {
                    return _context.StoreInventorySet.FromSql(query).First();
                }
            }
            catch (Exception) {
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
        public DataObject.StoreInventory CreateStoreInventory(long StoreID, long ProductID) {
            try {
                string query = " INSERT INTO StoreInventory " +
                    " (StoreID,ProductID,StockLevel) VALUES " +
                    $" ({StoreID},{ProductID},'1') ";
                // TODO runquery

                return GetStoreInventoryByStoreIdAndProductId(StoreID, ProductID); ;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        #endregion

        #region patch
        /// <summary>
        /// Update the quantity 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public DataObject.StoreInventory UpdateStoreInventory(long ProductID, long StoreID, long Quantity) {
            try {
                string query = $" UPDATE StoreInventory SET StockLevel = '{Quantity}' WHERE ProductID = '{ProductID}' AND StoreID = '{StoreID}'; ";
                // TODO runquery

                return new DataObject.StoreInventory();
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

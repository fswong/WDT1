﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class OwnerInventoryRepository : Repository
    {
        #region ctor
        public OwnerInventoryRepository(UnitOfWork uow) : base(uow: uow) { }
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
                return _context.OwnerInventorySet.FromSql(query).ToList();
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
        public DataObject.OwnerInventory GetOwnerInventoryById(int id, bool allowedNotFound = false) {
            try {
                string query = $" SELECT oi.*,p.Name FROM OwnerInventory oi " +
                    " INNER JOIN Product p ON oi.ProductID = p.ProductID " +
                    $" WHERE oi.ProductID = '{id}' ";
                if (allowedNotFound)
                {
                    return _context.OwnerInventorySet.FromSql(query).FirstOrDefault();
                }
                else {
                    return _context.OwnerInventorySet.FromSql(query).First();
                }
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
                _context.Database.ExecuteSqlCommand(query);
                _context.SaveChanges();

                return GetOwnerInventoryById(ProductID);
            } catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

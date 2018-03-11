using Microsoft.EntityFrameworkCore;
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
        public List<DataObject.StoreInventory> GetStoreInventoryByStoreId(long StoreID, bool inInventory) {
            try
            {
                string query = " SELECT si.* FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductId = p.ProductId ";
                return _context.StoreInventorySet.FromSql(query).ToList();
            }
            catch (Exception) {
                throw;
            }
        }

        public DataObject.StoreInventory GetStoreInventoryByStoreIdAndProductId(long StoreID, long ProductID, bool allowedNotFound = false) {
            try
            {
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

        #region patch
        public DataObject.StoreInventory UpdateStoreInventory(long ProductID, long Quantity) {
            try {
                string query = $" UPDATE StoreInventory SET StockLevel = '{Quantity}' WHERE ProductID = '{ProductID}' ";
                return new DataObject.StoreInventory();
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

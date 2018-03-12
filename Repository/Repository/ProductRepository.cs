using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProductRepository : Repository
    {
        #region properties
        #endregion

        #region ctor
        public ProductRepository(UnitOfWork uow) : base(uow:uow) { }
        #endregion

        #region get
        /// <summary>
        /// generic get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject.Product GetProductById(long id, bool allowedNotfound = false) {
            try {
                string query = $" SELECT * FROM Product WHERE ProductID='{id}' ";
                if (allowedNotfound) {
                    return _context.ProductSet.FromSql(query).FirstOrDefault();
                }
                else {
                    return _context.ProductSet.FromSql(query).First();
                }
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Returns list of products available/unavailable in store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="inInventory"></param>
        /// <returns></returns>
        public List<DataObject.Product> GetInventoryProduct(long storeId, bool inInventory) {
            try
            {
                string query = " SELECT * FROM Product WHERE Id ";
                if (inInventory) {
                    query += " IN ";
                }
                else {
                    query += " NOT IN ";
                }
                query += $" (SELECT DISTINCT ProductId FROM Inventory WHERE StoreId = '{storeId}') ";
                return _context.ProductSet.FromSql(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        #endregion

        #region patch

        #endregion
    }
}

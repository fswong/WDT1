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
        public ProductRepository(Transaction trn) : base(trn:trn) { }
        #endregion

        #region get
        /// <summary>
        /// generic get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject.Product GetProductById(long id) {
            try {
                string query = $" SELECT * FROM Product WHERE ProductID='{id}' ";
                //_transaction.RunQuery<DataObject.Product>(query);
                return _transaction._context.Database.SqlQuery<DataObject.Product>(query).First();
            }
            catch (Exception) {
                throw;
            }
            //return new DataObject.Product();
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
                return new List<DataObject.Product>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DataObject.Product> GetOwnerInventory()
        {
            try
            {
                string query = " SELECT * FROM Product WHERE ";
                return new List<DataObject.Product>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region patch
        public void ResetInventoryItemStock(long id) {

        }
        #endregion
    }
}

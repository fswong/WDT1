using DataObject.Extension;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository
{
    public class StoreRepository
    {
        #region ctor
        public StoreRepository(){}
        #endregion

        #region get
        /// <summary>
        /// get list of all stores
        /// </summary>
        /// <returns></returns>
        public List<DataObject.Store> ListStores() {
            try {
                string query = " SELECT * FROM Store ";
                return DBConn.GetDataTable(query).ToStoreListPOCO();
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Generic get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject.Store GetStoreById(int StoreID) {
            try {
                string query = $" SELECT * FROM STORE WHERE StoreID = '{StoreID}' ";
                return DBConn.Select(query).ToStorePOCO();
            } catch (Exception){
                throw;
            }
        }
        #endregion
    }
}

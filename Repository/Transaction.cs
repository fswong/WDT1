using System;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Repository
{
    public class Transaction
    {
        #region properties
        private string _connString = "";
        public DbContext _context;
        #endregion



        #region ctor
        /// <summary>
        /// generic ctor
        /// </summary>
        public Transaction() {
            try {
                _context = new DbContext(_connString);
            } catch (Exception e) {
                throw;
            }
        }
        #endregion

        #region methods
        //public T RunQuery<T>(string query){
        //    return _context.Database.SqlQuery<T>(query).First();
        //}
        #endregion
    }
}

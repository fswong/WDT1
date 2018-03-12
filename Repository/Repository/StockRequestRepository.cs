using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StockRequestRepository : Repository
    {
        #region ctor
        public StockRequestRepository(UnitOfWork uow) : base(uow:uow) { }
        #endregion

        #region get
        /// <summary>
        /// lists all stockrequests
        /// </summary>
        /// <returns></returns>
        public List<DataObject.StockRequest> ListStockRequests() {
            try {
                string query = " SELECT * FROM StockRequest ";
                return _context.StockRequestSet.FromSql(query).ToList();
            } catch (Exception) {
                throw;
            }
        }

        public DataObject.StockRequest GetStockRequestById(long StockRequestID, bool allowNotFound = false) {
            try {
                // TODO fix query
                string query = $" SELECT * FROM StockRequest WHERE StockRequestID = '{StockRequestID}' ";
                if (allowNotFound) {
                    return _context.StockRequestSet.FromSql(query).FirstOrDefault();
                }
                else {
                    return _context.StockRequestSet.FromSql(query).First();
                }
            } catch (Exception) {
                throw;
            }
        }
        #endregion

        #region delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StockRequestID"></param>
        public void DeleteStockRequest(long StockRequestID)
        {
            try
            {
                string query = $" SELECT * FROM StockRequest WHERE StockRequestID = '{StockRequestID}' ";
                // TODO runquery
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

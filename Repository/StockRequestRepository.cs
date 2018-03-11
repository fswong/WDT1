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
        public List<DataObject.StockRequest> ListStockRequests() {
            try {
                string query = " SELECT * from StockRequest ";
                return _context.StockRequestSet.FromSql(query).ToList();
            } catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

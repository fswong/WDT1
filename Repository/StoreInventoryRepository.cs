using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StoreInventory : Repository
    {
        #region ctor
        public StoreInventory(UnitOfWork uow) : base(uow:uow) { }
        #endregion

        #region get
        public List<StoreInventory> GetStoreInventoryByStoreId(long id) {
            try
            {
                string query = " SELECT si.* FROM StoreInventory si " +
                    " INNER JOIN Product p ON si.ProductId = p.ProductId ";
                return _context.StoreInventorySet.FromSql(query).List();
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class OwnerInventoryRepository : Repository
    {
        #region ctor
        public OwnerInventoryRepository(Transaction trn) : base (trn:trn){}
        #endregion

        #region get
        public List<OwnerInventoryRepository> GetByStoreId(long id) {
            try {
                string query = " SELECT oi.*, p.Nane, s.Name FROM " +
                    " OwnerInventory oi INNER JOIN " +
                    " Product p ON oi.ProductID = p.ProductID "
                return new List<OwnerInventoryRepository>();
            } catch (Exception e) {
                throw;
            }
        }
        #endregion

        #region patch
        #endregion
    }
}

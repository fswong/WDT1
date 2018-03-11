using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class StoreRepository : Repository
    {
        #region ctor
        public StoreRepository(UnitOfWork uow) : base(uow:uow) {}
        #endregion

        #region get
        /// <summary>
        /// Generic get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject.Store GetStoreById(long id) {
            try {
                return new DataObject.Store();
            } catch (Exception e){
                throw;
            }
        }
        #endregion
    }
}

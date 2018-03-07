using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class Store : BusinessObject
    {
        #region properties
        public DataObject.Store _poco { get; protected set; }

        private StoreRepository _repository;
        private List<DataObject.Product> _inventory;
        private List<DataObject.Product> _notInventory;
        #endregion

        #region ctor
        /// <summary>
        /// Constructor, returns the poco and products available in store
        /// </summary>
        /// <param name="trn"></param>
        /// <param name="id"></param>
        public Store(Transaction trn, long id) : base(trn: trn) {
            _repository = new StoreRepository(trn);
            _poco = _repository.GetStoreById(id);
            _inventory = GetInventoryProducts(true);
            _notInventory = GetInventoryProducts(false);
        }
        #endregion

        #region methods
        public List<DataObject.Product> GetInventoryProducts(bool inInventory = true) {
            try {
                return new List<DataObject.Product>();
            }
            catch (Exception e) {
                throw;
            }
        }
        #endregion
    }
}

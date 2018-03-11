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
        private List<DataObject.StoreInventory> _inventory;
        private List<DataObject.StoreInventory> _notInventory;
        #endregion

        #region ctor
        /// <summary>
        /// Constructor, returns the poco and products available in store
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="id"></param>
        public Store(UnitOfWork uow, long id) : base(uow: uow) {
            _repository = new StoreRepository(uow);
            _poco = _repository.GetStoreById(id);
            _inventory = GetInventoryProducts(true);
            _notInventory = GetInventoryProducts(false);
        }
        #endregion

        #region methods
        public List<DataObject.StoreInventory> GetInventoryProducts(bool inInventory = true) {
            try {
                return new List<DataObject.StoreInventory>();
            }
            catch (Exception e) {
                throw;
            }
        }
        #endregion
    }
}

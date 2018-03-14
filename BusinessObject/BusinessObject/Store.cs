using Common.Enum;
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
        public Store(UnitOfWork uow, int StoreID, UserType UserType) : base(uow: uow) {
            _repository = new StoreRepository();
            _poco = _repository.GetStoreById(StoreID);
            _inventory = GetInventoryProducts(true);
            _notInventory = GetInventoryProducts(false);
        }
        #endregion

        #region methods
        /// <summary>
        /// Used to populate the store's inventory
        /// </summary>
        /// <param name="inInventory"></param>
        /// <returns></returns>
        public List<DataObject.StoreInventory> GetInventoryProducts(bool inInventory = true) {
            try {
                return new StoreInventoryRepository(_context).GetStoreInventoryByStoreId(_poco.StoreID, inInventory);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        #endregion
    }
}

using Common;
using Common.Enum;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    // technically this is the only BO in this whole project
    public class Store : BusinessObject
    {
        #region properties
        public DataObject.Store _poco { get; protected set; }

        private StoreRepository _repository;
        private List<DataObject.StoreInventory> _inventory;
        #endregion

        #region ctor
        /// <summary>
        /// Constructor, returns the poco and products available in store
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="id"></param>
        public Store(int StoreID) : base(Id: StoreID) {
            _repository = new StoreRepository();
            _poco = _repository.GetStoreById(StoreID);
            _inventory = GetInventoryProducts(true);
        }
        #endregion

        #region methods

        /// <summary>
        /// Used to populate the store's inventory
        /// </summary>
        /// <param name="inInventory"></param>
        /// <returns></returns>
        private List<DataObject.StoreInventory> GetInventoryProducts(bool inInventory = true) {
            try {
                return new StoreInventoryRepository().GetStoreInventoryByStoreId(_poco.StoreID, inInventory);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// lists inventory
        /// </summary>
        public List<DataObject.StoreInventory> ListStoreInventory() {
            return _inventory;
        }

        /// <summary>
        /// lists not in inventory
        /// </summary>
        public List<DataObject.Product> ListNotInInventory()
        {
            try
            {
                return new ProductRepository().GetNotInInventory(_poco.StoreID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// create a new entry for a product in the inventory
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="StoreID"></param>
        /// <param name="Quantity"></param>
        public void AddToInventory(int ProductID, int Quantity = Constants.NEWSTOCKITEMDEFAULT) {
            //insert product into inventory
            try
            {
                new StoreInventoryRepository().CreateStoreInventory(ProductID: ProductID, StoreID: _poco.StoreID, Quantity: Quantity);
                _inventory = GetInventoryProducts(true);
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// displays list of stock below threshold
        /// </summary>
        /// <param name="Threshold"></param>
        /// <returns></returns>
        public List<DataObject.StoreInventory> DisplayStockThreshold(int Threshold) {
            try {
                var stock = new StoreInventoryRepository().GetStoreRequestByStoreIdAndThreshold(StoreID: _poco.StoreID, Threshold: Threshold);
                return stock;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// creates a store request for the product
        /// </summary>
        /// <param name="Threshold"></param>
        /// <param name="ProductID"></param>
        public void RequestStock(int Threshold, int ProductID) {
            try
            {
                new StockRequestRepository().CreateStockRequest(StoreID: _poco.StoreID, ProductID: ProductID, Quantity: Threshold);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// purchase the itme for the customer
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        public void PurchaseStock(int ProductID, int Quantity) {
            try {

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}

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
        private List<DataObject.StoreInventory> _notInventory;
        #endregion

        #region ctor
        /// <summary>
        /// Constructor, returns the poco and products available in store
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="id"></param>
        public Store(int StoreID, UserType UserType) : base(Id: StoreID) {
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
        public List<DataObject.StoreInventory> GetInventoryProducts(bool inInventory = true) {
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
        public void DisplayInventory() {
            //display
            Console.WriteLine("Stock Requests");
            Console.WriteLine(" ");

            string headerRow = "Id  " +
                "" +
                "" +
                "" +
                "" +
                "";
            Console.WriteLine(headerRow);
            foreach (var item in _inventory) {

            }
        }

        /// <summary>
        /// lists inventory
        /// </summary>
        public List<DataObject.Product> DisplayNotInInventory()
        {
            //display
            Console.WriteLine("Add to Inventory");
            Console.WriteLine(" ");

            var NotInInventory = new ProductRepository().GetNotInInventory(_poco.StoreID);

            string headerRow = "Id" +
                "" +
                "" +
                "" +
                "" +
                "";
            Console.WriteLine(headerRow);
            foreach (var item in _inventory)
            {

            }

            return NotInInventory;
        }
        #endregion
    }
}

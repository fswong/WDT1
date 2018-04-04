using BusinessObject;
using Common.Enum;
using Common.StaticMethods;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction
{
    public class Store
    {
        #region ctor
        // generic constructor
        public Store() { }
        #endregion

        #region methods

        /// <summary>
        /// idk why this is here, this is technically just to follow hte architecture, the user already has access to the 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public List<DataObject.StoreInventory> ListStoreProducts(BusinessObject.Store store) {
            try {
                return store.ListStoreInventory();
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// if the item does not exist in the store add the item with one quantity
        /// </summary>
        /// <param name="store"></param>
        /// <param name="ProductID"></param>
        public BusinessObject.Store AddNewInventoyItem(BusinessObject.Store store, int ProductID) {
            try
            {
                store.AddToInventory(ProductID);
                return store;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// create a store request
        /// </summary>
        /// <param name="store"></param>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        public void RequestStock(BusinessObject.Store store, int ProductID, int Quantity) {
            try
            {
                // TODO pass this to the BO
                new StockRequestRepository().CreateStockRequest(store._poco.StoreID, ProductID, Quantity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// if there is sufficient quantity in the store, reduce the store quantity
        /// </summary>
        /// <param name="store"></param>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public BusinessObject.Store PurchaseItem(BusinessObject.Store store, int ProductID, int Quantity) {
            try
            {
                // TODO pass this to the BO
                //get the item from the store
                var siRepo = new StoreInventoryRepository();
                var item = siRepo.GetStoreInventoryByStoreIdAndProductId(StoreID: store._poco.StoreID, ProductID: ProductID);

                // validate that there is enough stock
                if (item.StockLevel >= Quantity)
                {
                    int newStock = item.StockLevel - Quantity;
                    siRepo.UpdateStoreInventory(StoreID: store._poco.StoreID, ProductID: ProductID, Quantity: newStock);

                    // refresh the store's inventory
                    // this is the stores internal private method, so this logic has to move into the store
                }
                else {
                    throw new Exception("Insuffienct stock to process thsi request");
                }
                return store;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// returns a list for the franchise holder to view
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public List<DataObject.StockRequest> ListStockRequests(int StoreID) {
            try
            {
                return new StockRequestRepository().ListStockRequests(StoreID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region static!!!
        /// <summary>
        /// lists the available stores
        /// </summary>
        public static BusinessObject.Store DisplayStoreList()
        {
            try
            {
                var stores = new StoreRepository().ListStores();

                Console.WriteLine("Stores");
                Console.WriteLine(" ");

                if (stores == null)
                {
                    stores = new StoreRepository().ListStores();
                }

                foreach (var obj in stores)
                {
                    string row = obj.StoreID.ToString().PadRight((int)Padding.id) +
                        obj.Name.PadRight((int)Padding.name);
                    Console.WriteLine(row);
                }

                Console.WriteLine(" ");
                Console.Write("Enter an option: ");

                var input = Console.ReadLine();
                int inputParsed;
                CommonFunctions.TryParseInt(input, out inputParsed);

                if (inputParsed < stores.Count)
                {
                    return new BusinessObject.Store(inputParsed);
                }
                else {
                    throw new Exception("Invalid Store Choesen");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

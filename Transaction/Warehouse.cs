using Common;
using Common.Enum;
using Common.Widgets;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction
{
    public class Warehouse
    {
        #region properties
        private List<DataObject.StockRequest> _stockRequests;
        private List<DataObject.OwnerInventory> _ownerInventory;
        #endregion

        #region ctor
        public Warehouse()
        {
            // get the stock requests
            _stockRequests = new StockRequestRepository().ListStockRequests();

            // get the owner inventory
            _ownerInventory = new OwnerInventoryRepository().ListOwnerInventory();
        }
        #endregion

        #region methods

        /// <summary>
        /// Returns a list of all stock requests
        /// </summary>
        /// <returns></returns>
        public List<DataObject.StockRequest> ListAllStockRequests() {
            try {
                return _stockRequests;
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// returns a list of all owner inventory
        /// </summary>
        /// <returns></returns>
        public List<DataObject.OwnerInventory> ListAllOwnerInventory() {
            try
            {
                return _ownerInventory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// updates the stock of a product to 20 if the stock is less than 20
        /// </summary>
        /// <param name="productID"></param>
        public void ProcessOwnerInventory(int productID) {
            //get the product
            var product = _ownerInventory.Find(item => item.ProductID == productID);

            //validate that the product is valid
            if (product != null)
            {
                //check that the stock level is below max
                if (product.StockLevel < Constants.OWNERMAXSTOCK)
                {
                    var oiRepo = new OwnerInventoryRepository();
                    try
                    {
                        //update the product
                        product = oiRepo.UpdateOwnerInventory(product.ProductID, Constants.OWNERMAXSTOCK);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    Console.WriteLine(product.ProductID + " stocklevel has been reset to " + product.StockLevel);
                }
                else
                {
                    WidgetError.DisplayError(product.Name + " already has enough stock");
                }
            }
            else
            {
                WidgetError.DisplayError("No such product found");
            }
        }

        /// <summary>
        /// if valid, transfers from the owners stock to the relavant store
        /// </summary>
        /// <param name="stockRequestID"></param>
        public void ProcessStockRequest(int stockRequestID)
        {
            try
            {
                var StockRequest = new StockRequestRepository().GetStockRequestById(stockRequestID);
                var product = _ownerInventory.Find(item => item.ProductID == StockRequest.ProductID);

                if (product != null)
                {
                    // validate
                    if (product.StockLevel > StockRequest.Quantity && StockRequest.StockAvailability)
                    {
                        product.StockLevel = product.StockLevel - StockRequest.Quantity;
                        product = new OwnerInventoryRepository().UpdateOwnerInventory(product.ProductID, product.StockLevel);

                        //add store stock
                        var siRepo = new StoreInventoryRepository();
                        var storeInventory = siRepo.GetStoreInventoryByStoreIdAndProductId(StockRequest.StoreID, product.ProductID);
                        storeInventory.StockLevel = storeInventory.StockLevel + StockRequest.Quantity;
                        siRepo.UpdateStoreInventory(product.ProductID, StockRequest.StoreID, storeInventory.StockLevel);

                        //delete stock request
                        new StockRequestRepository().DeleteStockRequest(StockRequest.StockRequestID);
                    }
                    else
                    {
                        WidgetError.DisplayError("Insufficient stock to process thsi request");
                    }
                }
                else
                {
                    WidgetError.DisplayError("Invalid product chosen");
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

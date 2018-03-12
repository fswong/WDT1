using Common.Enum;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Owner : User 
    {
        #region properties
        // TODO change this to column type
        private const int col1 = 4;
        private const int col2 = 4;
        private const int col3 = 4;
        private const int col4 = 4;
        private const int col5 = 4;
        private const int col6 = 4;

        private const int _MAXSTOCK = 20;

        public List<DataObject.StockRequest> _stockRequests { get; protected set; }
        public List<DataObject.OwnerInventory> _ownerInventory { get; protected set; }
        #endregion

        #region ctor
        public Owner(UnitOfWork uow) : base(uow:uow) {
            // get the stock requests
            _stockRequests = new StockRequestRepository(_context).ListStockRequests();

            // get the owner inventory
            _ownerInventory = new OwnerInventoryRepository(_context).ListOwnerInventory();

            // begin transaction
            Action();
        }
        #endregion

        #region methods
        /// <summary>
        /// user's main view
        /// </summary>
        public override void DisplayUserMenu() {
            Console.WriteLine("Welcome to Marvelous Magic (Owner)");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Display All Stock Requests");
            Console.WriteLine("2. Display Owner Inventory");
            Console.WriteLine("3. Reset Inventory Item Stock");
            Console.WriteLine("4. Return to Main Menu");
            Console.WriteLine(" ");
            Console.Write("Enter an option:");

            try {
                var response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        DisplayAllStockRequests();
                        break;
                    case "2":
                        DisplayOwnerInventory();
                        break;
                    case "3":
                        ResetInventoryItemStock();
                        break;
                    case "4":
                        _state = State.closed;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        //DisplayUserMenu();
                        break;

                }
            } catch (Exception e) {
                Console.WriteLine(e);
                //DisplayUserMenu();
            }
        }

        /// <summary>
        /// Returns a list of all stock requests
        /// </summary>
        /// <returns></returns>
        public void DisplayAllStockRequests()
        {
            try
            {
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

                IEnumerable<DataObject.StockRequest> result = _stockRequests;

                foreach (var item in result)
                {
                    string outputRow =
                    item.StockRequestID.ToString().PadRight((int)Padding.id,' ') +
                    item.StoreName.PadRight((int)Padding.name, ' ') +
                    item.ProductName.PadRight((int)Padding.name, ' ') +
                    item.Quantity.ToString().PadRight((int)Padding.quantity, ' ') +
                    item.CurrentStock.ToString().PadRight((int)Padding.quantity, ' ') +
                    item.StockAvailability.ToString().PadRight((int)Padding.boolean, ' ');
                    Console.WriteLine(outputRow);
                }

                Console.WriteLine(" ");
                Console.WriteLine("Enter request to process:");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //DisplayUserMenu();
            }
        }

        /// <summary>
        /// lists owner inventory
        /// </summary>
        public void DisplayOwnerInventory()
        {
            try
            {
                //generate header
                Console.WriteLine("Owner Inventory");
                Console.WriteLine(" ");

                DisplayOwnerInventoryView();

                Console.WriteLine(" ");

                //DisplayUserMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //DisplayUserMenu();
            }
        }

        /// <summary>
        /// if item has less than 20 stock display
        /// </summary>
        public void ResetInventoryItemStock()
        {
            try
            {
                //generate header
                Console.WriteLine("Reset Stock");
                Console.WriteLine("Product stock will be reset to " + _MAXSTOCK);
                Console.WriteLine(" ");

                DisplayOwnerInventoryView();

                Console.WriteLine(" ");
                Console.WriteLine("Enter product ID to reset:");

                var input =Console.ReadLine();

                var product = _ownerInventory.Find(item => item.ProductID.ToString() == input);

                if (product.StockLevel < _MAXSTOCK)
                {
                    product = new OwnerInventoryRepository(_context).UpdateOwnerInventory(product.ProductID, _MAXSTOCK);
                    Console.WriteLine(product.ProductID + " stocklevel has been reset to " + _MAXSTOCK);
                }
                else {
                    Console.WriteLine(product.ProductID + " already has enough stock");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //DisplayUserMenu();
            }
        }

        /// <summary>
        /// Completes a stockrequest
        /// </summary>
        /// <param name="StockRequest"></param>
        public void FulfillStockRequest(DataObject.StockRequest StockRequest) {
            try {
                var product = _ownerInventory.Find(item => item.ProductID == StockRequest.ProductID);

                if (product != null)
                {
                    // validate
                    if (product.StockLevel > StockRequest.Quantity && StockRequest.StockAvailability)
                    {
                        //reduce the owner stock
                        product.StockLevel = product.StockLevel - StockRequest.Quantity;
                        product = new OwnerInventoryRepository(_context).UpdateOwnerInventory(product.ProductID, product.StockLevel);

                        //add store stock
                        var siRepo = new StoreInventoryRepository(_context);
                        var storeInventory = siRepo.GetStoreInventoryByStoreIdAndProductId(StockRequest.StoreID, product.ProductID);
                        storeInventory.StockLevel = storeInventory.StockLevel + StockRequest.Quantity;
                        siRepo.UpdateStoreInventory(product.ProductID, StockRequest.StoreID, storeInventory.StockLevel);

                        //delete stock request
                        new StockRequestRepository(_context).DeleteStockRequest(StockRequest.StockRequestID);
                    }
                }
                else {
                    Console.WriteLine("Invalid product chosen");
                }

                //DisplayUserMenu();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                //DisplayUserMenu();
            }

        }
        #endregion

        #region view
        /// <summary>
        /// owner inventory view
        /// </summary>
        public void DisplayOwnerInventoryView() {
            try {
                //generic header
                string header = "ID Product Current Stock";
                Console.WriteLine(header);

                //generate details
                foreach (var item in _ownerInventory)
                {
                    string outputRow =
                        item.ProductID.ToString().PadRight((int)Padding.id, ' ') +
                        item.Name.PadRight((int)Padding.name, ' ') +
                        item.StockLevel.ToString().PadRight((int)Padding.quantity, ' ');
                    Console.WriteLine(outputRow);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                //DisplayUserMenu();
            }
            
        }
        #endregion
    }
}

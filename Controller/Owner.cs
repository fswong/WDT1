using Common.Enum;
using Common.StaticMethods;
using Common.Widgets;
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
        public Owner() : base() {
            // get the stock requests
            _stockRequests = new StockRequestRepository().ListStockRequests();

            // get the owner inventory
            _ownerInventory = new OwnerInventoryRepository().ListOwnerInventory();

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

                string[] header = { "ID", "Store Name", "Product Name", "Quantity", "Current Stock", "Availability" };
                header[0] = header[0].PadRight((int)Padding.id, ' ');
                header[1] = header[1].PadRight((int)Padding.name, ' ');
                header[2] = header[2].PadRight((int)Padding.name, ' ');
                header[3] = header[3].PadRight((int)Padding.quantity, ' ');
                header[4] = header[4].PadRight((int)Padding.quantity, ' ');
                header[5] = header[5].PadRight((int)Padding.boolean, ' ');

                string headerString = "";
                foreach (string str in header)
                {
                    headerString += str;
                }

                Console.WriteLine(headerString);

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

                //display the table
                DisplayOwnerInventoryView();

                Console.WriteLine(" ");
                Console.Write("Enter product ID to reset:");

                var input =Console.ReadLine();
                int inputParsed;

                // validate that the input was integer
                if (CommonFunctions.TryParseInt(input, out inputParsed))
                {
                    //get the product
                    var product = _ownerInventory.Find(item => item.ProductID.ToString() == input);

                    //validate that the product is valid
                    if (product != null)
                    {
                        //check that the stock level is below max
                        if (product.StockLevel < _MAXSTOCK)
                        {
                            var oiRepo = new OwnerInventoryRepository();
                            try
                            {
                                //update the product
                                product = oiRepo.UpdateOwnerInventory(product.ProductID, _MAXSTOCK);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            //refresh the list
                            _ownerInventory = oiRepo.ListOwnerInventory();
                            Console.WriteLine(product.ProductID + " stocklevel has been reset to " + product.StockLevel);
                        }
                        else
                        {
                            Console.WriteLine(product.Name + " already has enough stock");
                        }
                    }
                    else {
                        Console.WriteLine("No such product found");
                    }
                }
                else {
                    Console.WriteLine("Invalid Input");
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                        //using (var uow = new UnitOfWork()) {
                        //    try
                        //    {


                        //    }
                        //    catch (Exception e)
                        //    {
                        //        Console.WriteLine(e.Message);
                        //    }
                        //    finally
                        //    {
                        //        uow.Dispose();
                        //        uow.Close();
                        //    }
                        //}
                            

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
                    else {

                    }
                }
                else {
                    Console.WriteLine("Invalid product chosen");
                }

                //DisplayUserMenu();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        #region inherited
        /// <summary>
        /// owner inventory view
        /// </summary>
        public void DisplayOwnerInventoryView() {
            try {
                var headers = new List<string>();
                var content = new List<string>();
                var footer = " ";

                //generate header
                string[] header = { "ID", "Product", "Current Stock" };
                header[0] = header[0].PadRight((int)Padding.id, ' ');
                header[1] = header[1].PadRight((int)Padding.name, ' ');
                header[2] = header[2].PadRight((int)Padding.quantity, ' ');

                string headerString = "";
                foreach (string str in header) {
                    headerString += str;
                }

                headers.Add(headerString);

                //generate details
                foreach (var item in _ownerInventory)
                {
                    string outputRow =
                        item.ProductID.ToString().PadRight((int)Padding.id, ' ') +
                        item.Name.PadRight((int)Padding.name, ' ') +
                        item.StockLevel.ToString().PadRight((int)Padding.quantity, ' ');
                    content.Add(outputRow);
                }

                //var obj = new WidgetPaging(headers, content, footer);
                WidgetTable.DisplayTable(headers, content, footer);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
        }
        #endregion
    }
}

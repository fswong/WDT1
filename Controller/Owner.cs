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
        private const int col1 = 4;
        private const int col2 = 4;
        private const int col3 = 4;
        private const int col4 = 4;
        private const int col5 = 4;
        private const int col6 = 4;

        public List<DataObject.StockRequest> _stockRequests { get; protected set; }
        public List<DataObject.OwnerInventory> _ownerInventory { get; protected set; }
        #endregion

        #region ctor
        public Owner() : base() {
            // get the stock requests
            _stockRequests = new StockRequestRepository(_context).ListStockRequests();
            // get the owner inventory
            _ownerInventory = new OwnerInventoryRepository(_context).ListOwnerInventory();
        }
        #endregion

        #region methods
        override
        public void DisplayUserMenu() {
            Console.WriteLine("Welcome to Marvelous Magic (Owner)");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Display All Stock Requests");
            Console.WriteLine("2. Display Owner Inventory");
            Console.WriteLine("3. Reset Inventory Item Stock");
            Console.WriteLine("4. Return to Main Menu");
            Console.WriteLine(" ");
            Console.WriteLine("Enter an option:");

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
                        DisplayUserMenu();
                        break;

                }
            } catch (Exception e) {
                Console.WriteLine(e);
                DisplayUserMenu();
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
                    item.StockRequestID.ToString().PadRight(col1,' ') +
                    item.StoreName.PadRight(col2, ' ') +
                    item.ProductName.PadRight(col3, ' ') +
                    item.Quantity.ToString().PadRight(col4, ' ') +
                    item.CurrentStock.ToString().PadRight(col5, ' ') +
                    item.StockAvailability.ToString().PadRight(col6, ' ');
                    Console.WriteLine(outputRow);
                }

                Console.WriteLine(" ");
                Console.WriteLine("Enter request to process:");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw;
            }
        }

        public void DisplayOwnerInventory()
        {
            try
            {
                foreach (var obj in _ownerInventory) {
                    string outputRow = "";
                    Console.WriteLine(outputRow);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void ResetInventoryItemStock()
        {
            try
            {

            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
    }
}

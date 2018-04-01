using Common;
using Common.Enum;
using Common.StaticMethods;
using Common.Widgets;
using System;
using System.Collections.Generic;
using Transaction;

namespace Controller
{
    public class Owner : User 
    {
        #region ctor
        public Owner() : base() {
            // begin transaction
            Action();
        }
        #endregion

        #region event handlers

        /// <summary>
        /// owners's main view
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
                int responseParsed;
                if (CommonFunctions.TryParseInt(response, out responseParsed))
                {
                    switch ((OwnerMenu)responseParsed)
                    {
                        case OwnerMenu.displaystockrequest:
                            DisplayAllStockRequests();
                            break;
                        case OwnerMenu.displayownerinventory:
                            DisplayOwnerInventory();
                            break;
                        case OwnerMenu.resetitemstock:
                            ResetInventoryItemStock();
                            break;
                        case OwnerMenu.back:
                            _state = State.closed;
                            break;
                        default:
                            WidgetError.DisplayError("Invalid Input");
                            break;
                    }
                }
                else {
                    WidgetError.DisplayError("Invalid Input");
                }
            } catch (Exception) {
                WidgetError.DisplayError("Invalid Input");
            }
        }

        /// <summary>
        /// 1. returns a list of all stock requests
        /// </summary>
        /// <returns></returns>
        private void DisplayAllStockRequests()
        {
            try
            {
                var transaction = new Warehouse();
                var requests = transaction.ListAllStockRequests();

                DisplayAllStockRequestsView(requests);

                var input = Console.ReadLine();
                int inputParsed;

                // validate that the input was integer
                if (CommonFunctions.TryParseInt(input, out inputParsed))
                {
                    transaction.ProcessStockRequest(inputParsed);
                }
                else
                {
                    WidgetError.DisplayError("Invalid Input");
                }
            }
            catch (Exception e)
            {
                WidgetError.DisplayError(e.Message);
            }
        }

        /// <summary>
        /// 2. lists owner inventory
        /// </summary>
        private void DisplayOwnerInventory()
        {
            try
            {
                var items = new Warehouse().ListAllOwnerInventory();

                //generate header
                Console.WriteLine("Owner Inventory");
                Console.WriteLine(" ");

                DisplayOwnerInventoryView(items);

                Console.WriteLine(" ");
            }
            catch (Exception e)
            {
                WidgetError.DisplayError(e.Message);
            }
        }

        /// <summary>
        /// 3. if item has less than 20 stock restock to 20
        /// </summary>
        private void ResetInventoryItemStock()
        {
            try
            {
                var transaction = new Warehouse();
                var items = transaction.ListAllOwnerInventory();

                //generate header
                Console.WriteLine("Reset Stock");
                Console.WriteLine("Product stock will be reset to " + Constants.OWNERMAXSTOCK);
                Console.WriteLine(" ");

                //display the table
                DisplayOwnerInventoryView(items);

                Console.WriteLine(" ");
                Console.Write("Enter product ID to reset:");

                var input =Console.ReadLine();
                int inputParsed;

                // validate that the input was integer
                if (CommonFunctions.TryParseInt(input, out inputParsed))
                {
                    transaction.ProcessOwnerInventory(inputParsed);
                }
                else {
                    WidgetError.DisplayError("Invalid Input");
                }
            }
            catch (Exception e)
            {
                WidgetError.DisplayError(e.Message);
            }
        }
        
        #endregion

        #region views

        /// <summary>
        /// displays stock requests
        /// </summary>
        /// <param name="items"></param>
        private void DisplayAllStockRequestsView(List<DataObject.StockRequest> items) {
            try {
                List<string> headers = new List<string>();
                List<string> content = new List<string>();
                string footer = "";

                // generate display
                headers.Add("Stock Requests");
                headers.Add(" ");

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

                headers.Add(headerString);

                foreach (var item in items)
                {
                    string outputRow =
                    item.StockRequestID.ToString().PadRight((int)Padding.id, ' ') +
                    item.StoreName.PadRight((int)Padding.name, ' ') +
                    item.ProductName.PadRight((int)Padding.name, ' ') +
                    item.Quantity.ToString().PadRight((int)Padding.quantity, ' ') +
                    item.CurrentStock.ToString().PadRight((int)Padding.quantity, ' ') +
                    item.StockAvailability.ToString().PadRight((int)Padding.boolean, ' ');
                    content.Add(outputRow);
                }

                footer = "Enter request to process:";

                WidgetTable.DisplayTable(headers, content, footer);
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// displays the owner inventory
        /// </summary>
        /// <param name="items"></param>
        private void DisplayOwnerInventoryView(List<DataObject.OwnerInventory> items) {
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
                foreach (var item in items)
                {
                    string outputRow =
                        item.ProductID.ToString().PadRight((int)Padding.id, ' ') +
                        item.Name.PadRight((int)Padding.name, ' ') +
                        item.StockLevel.ToString().PadRight((int)Padding.quantity, ' ');
                    content.Add(outputRow);
                }

                WidgetTable.DisplayTable(headers, content, footer);
            }
            catch (Exception) {
                throw;
            }
            
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using Common.Interface;
using Common.Enum;
using System.Linq;
using Common.Widgets;
using Common.StaticMethods;

namespace Controller
{
    public class FranchiseHolder : User, IStorefront
    {
        #region properties
        public BusinessObject.Store _store { get; protected set; }
        public List<DataObject.Store> _stores { get; protected set; }
        #endregion

        #region ctor
        public FranchiseHolder() : base(){
            Action();
        }
        #endregion

        #region handlers

        /// <summary>
        /// Returns list of items
        /// </summary>
        public void DisplayInventory() {
            try {
                var items = _store.ListStoreInventory();

            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// display stock requests for the current store
        /// </summary>
        public void StockRequestHandler()
        {
            try
            {
                var stockRequest = new StockRequestRepository().ListStockRequests(_store._poco.StoreID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds 1 unit of an item that is not in the inventory
        /// Quantity of item is set to zero
        /// </summary>
        public void AddNewInventoryItem()
        {
            try
            {
                // list stuff thats not in
                var notInInventory = _store.ListNotInInventory();

                // add to list
                var input = Console.ReadLine();
                int inputParsed;
                if (CommonFunctions.TryParseInt(input, out inputParsed))
                {
                    if (notInInventory.Any(item => item.ProductID == inputParsed))
                    {
                        _store.AddToInventory(ProductID: inputParsed);
                    }
                    else
                    {
                        WidgetError.DisplayError("Invalid Input");
                    }
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

        #region inherited
        
        /// <summary>
        /// initiate the class
        /// </summary>
        public override void Action()
        {
            try
            {
                do
                {
                    if (_store == null)
                    {
                        DisplayStoreList();
                    }
                    else
                    {
                        DisplayUserMenu();
                    }
                } while (_state != State.closed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        #endregion

        #region view

        /// <summary>
        /// the root franchise holder view
        /// </summary>
        public override void DisplayUserMenu()
        {
            //throw new NotImplementedException();
            Console.WriteLine("Welcome to Marvelous Magic (Franchise Holder)");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Display Inventory");
            Console.WriteLine("2. Display Stock Request");
            Console.WriteLine("3. Add New Inventory Item");
            Console.WriteLine("4. Return to Main Menu");
            Console.WriteLine(" ");
            Console.Write("Enter an option:");

            try
            {
                var response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        DisplayInventory();
                        break;
                    case "2":
                        StockRequestHandler();
                        break;
                    case "3":
                        AddNewInventoryItem();
                        break;
                    case "4":
                        _state = State.closed;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// lists the available stores
        /// </summary>
        public void DisplayStoreList()
        {
            try
            {
                //get the business object that is being interacted with
                _store = Transaction.Store.DisplayStoreList();
            }
            catch (Exception e)
            {
                WidgetError.DisplayError(e.Message);
            }
        }

        public void DisplayStoreInvontory(List<DataObject.StoreInventory> items) {
            try
            {
                var headers = new List<string>();
                var content = new List<string>();
                var footer = " ";

                //generate header
                string[] header = { "ID", "Product", "Current Stock" };
                header[0] = header[0].PadRight((int)Padding.id, ' ');
                header[1] = header[1].PadRight((int)Padding.name, ' ');
                header[2] = header[2].PadRight((int)Padding.quantity, ' ');

                string headerString = "";
                foreach (string str in header)
                {
                    headerString += str;
                }

                headers.Add(headerString);

                //generate details
                foreach (var item in items)
                {
                    string outputRow =
                        item.ProductID.ToString().PadRight((int)Padding.id, ' ') +
                        item.ProductName.PadRight((int)Padding.name, ' ') +
                        item.StockLevel.ToString().PadRight((int)Padding.quantity, ' ');
                    content.Add(outputRow);
                }

                WidgetTable.DisplayTable(headers, content, footer);
            }
            catch (Exception e) {
                WidgetError.DisplayError(e.Message);
            }
        }

        public void DisplayNotInInventory() {

        }

        #endregion
    }
}

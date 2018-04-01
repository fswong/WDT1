using System;
using System.Collections.Generic;
using System.Text;
using Common.Enum;
using Common.Interface;
using Common.StaticMethods;
using Common.Widgets;
using Repository;

namespace Controller
{
    public class Customer : User, IStorefront
    {
        #region properties
        private BusinessObject.Store _store;
        #endregion

        #region ctor
        /// <summary>
        /// generic constructor
        /// </summary>
        public Customer():base() {
            Action();
        }
        #endregion

        #region method

        //
        private void PurchaseProduct(int ProductID, int Quantity) {
            try {
                //reset the business object
                _store = new Transaction.Store().PurchaseItem(_store, ProductID, Quantity);
            } catch (Exception e) {
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

        #region views

        /// <summary>
        /// customer generic menu
        /// </summary>
        public override void DisplayUserMenu()
        {
            Console.WriteLine("Welcome to Marvelous Magic (Customer)");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Display Products");
            Console.WriteLine("2. Return to Main Menu");
            Console.WriteLine(" ");
            Console.Write("Enter an option:");

            try
            {
                var response = Console.ReadLine();
                int responseParsed;
                if (CommonFunctions.TryParseInt(response, out responseParsed))
                {
                    switch ((CustomerMenu)responseParsed)
                    {
                        case CustomerMenu.displayproducts:
                            DisplayProducts();
                            break;
                        case CustomerMenu.back:
                            _state = State.closed;
                            break;
                        default:
                            WidgetError.DisplayError("Invalid Input");
                            break;
                    }
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

        /// <summary>
        /// list store products
        /// </summary>
        public void DisplayProducts()
        {
            var headers = new List<string>();
            var content = new List<string>();
            var responses = new List<int>();
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

            var products = new StoreInventoryRepository().GetStoreInventoryByStoreId(_store._poco.StoreID, true);

            //generate details
            foreach (var item in products)
            {
                string outputRow =
                    item.ProductID.ToString().PadRight((int)Padding.id, ' ') +
                    item.ProductName.PadRight((int)Padding.name, ' ') +
                    item.StockLevel.ToString().PadRight((int)Padding.quantity, ' ');
                content.Add(outputRow);
            }

            var obj = new WidgetPaging(headers, content, footer, responses);
        }

        #endregion
    }
}

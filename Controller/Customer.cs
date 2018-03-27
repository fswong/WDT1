using System;
using System.Collections.Generic;
using System.Text;
using Common.Enum;
using Common.Interface;
using Common.Widgets;
using Repository;

namespace Controller
{
    public class Customer : User, IStorefront
    {
        #region properties
        public BusinessObject.Store _store { get; protected set; }
        public List<DataObject.Store> _stores { get; protected set; }
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
        public void DisplayProducts() {
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

        #region inherited
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
                switch (response)
                {
                    case "1":
                        DisplayProducts();
                        break;
                    case "2":
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

        /// <summary>
        /// lists the available stores
        /// </summary>
        public void DisplayStoreList()
        {
            try
            {
                Console.WriteLine("Stores");
                Console.WriteLine(" ");

                if (_stores == null)
                {
                    _stores = new StoreRepository().ListStores();
                }

                foreach (var obj in _stores)
                {
                    string row = obj.StoreID.ToString().PadRight((int)Padding.id) +
                        obj.Name.PadRight((int)Padding.name);
                    Console.WriteLine(row);
                }

                Console.WriteLine(" ");
                Console.Write("Enter an option: ");

                var input = Console.ReadLine();
                int inputParsed = Convert.ToInt32(input);

                if (inputParsed < _stores.Count)
                {
                    _store = new BusinessObject.Store(inputParsed);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
    }
}

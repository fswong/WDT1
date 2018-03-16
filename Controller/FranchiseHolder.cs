using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using Common.Interface;
using Common.Enum;

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

        #region methods
        /// <summary>
        /// Returns list of items
        /// </summary>
        /// <param name="inInventory">Toggles if in or not in inventory</param>
        /// <returns></returns>
        public List<DataObject.Product> DisplayInventory(bool inInventory = true) {
            try {

                return new List<DataObject.Product>();
            } catch (Exception e) {
                throw;
            }
        }

        public List<DataObject.Product> StockRequest()
        {
            try
            {

                return DisplayInventory();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds 1 unit of an item that is not in the inventory
        /// Quantity of item is set to zero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DataObject.Product> AddNewInventoryItem(int id)
        {
            try
            {
                // check that the item is not in the list

                // add to list

                // delete from other list
                return DisplayInventory();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

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
                        throw new NotImplementedException();
                        break;
                    case "2":
                        throw new NotImplementedException();
                        break;
                    case "3":
                        throw new NotImplementedException();
                        break;
                    case "4":
                        _state = State.closed;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        //DisplayUserMenu();
                        break;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //DisplayUserMenu();
            }
        }

        /// <summary>
        /// lists the available stores
        /// </summary>
        public void DisplayStoreList()
        {
            try {
                Console.WriteLine("Stores");
                Console.WriteLine(" ");

                if (_stores == null) {
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

                if (inputParsed < _stores.Count) {
                    _store = new BusinessObject.Store(inputParsed,UserType.franchiseholder);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// initiate the class
        /// </summary>
        public override void Action() {
            try {
                do {
                    if (_store == null)
                    {
                        DisplayStoreList();
                    }
                    else
                    {
                        DisplayUserMenu();
                    }
                } while (_state!=State.closed);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Common.Enum;
using Common.Interface;
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
                        throw new NotImplementedException();
                        break;
                    case "2":
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
                    _store = new BusinessObject.Store(inputParsed, UserType.franchiseholder);
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

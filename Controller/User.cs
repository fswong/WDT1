using System;
using System.Collections.Generic;
using System.Text;
using Common.Enum;
using Repository;

namespace Controller
{
    public abstract class User
    {
        #region properties
        protected State _state;
        protected UnitOfWork _context;
        #endregion

        #region ctor
        public User() {
            _state = State.open;
        }
        #endregion

        #region methods
        /// <summary>
        /// Generic return to main menu
        /// </summary>
        public void ReturnToMainMenu() {
            this._state = State.closed;
        }

        public abstract void DisplayUserMenu();

        /// <summary>
        /// User generic main menu
        /// </summary>
        public void Action() {
            using (var uow = new UnitOfWork()) {
                _context = uow;
                do
                {
                    try
                    {
                        DisplayUserMenu();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        DisplayUserMenu();
                    }
                } while (_state != State.closed);
            }
        }
        #endregion

        #region static!!!
        /// <summary>
        /// not sure where else to put this
        /// technically anyone who sees this is a user?
        /// </summary>
        /// <param name="driver"></param>
        public static void DisplayMainMenu(Driver driver)
        {
            Console.WriteLine("Welcome to Marvelous Magic");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Owner");
            Console.WriteLine("2. Franchise Holder");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Quit");
            User user = null;
            try
            {
                //validate the driver state
                if (driver.IsValid()) {
                    var response = Console.ReadLine();
                    //handle responses
                    switch (response)
                    {
                        case "1":
                            user = new Owner();
                            break;
                        case "2":
                            user = new FranchiseHolder();
                            break;
                        case "3":
                            user = new Customer();
                            break;
                        case "4":
                            driver.CloseApplication();
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            DisplayMainMenu(driver);
                            break;
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                DisplayMainMenu(driver);
            }
            finally
            {
                //displose of object
                if (user != null)
                {
                    ((IDisposable)user).Dispose();
                    //user = null;
                }
            }
        }
        #endregion
    }
}

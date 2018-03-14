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
        public User(UnitOfWork uow) {
            _state = State.open;
            _context = uow;
        }
        #endregion

        #region methods
        /// <summary>
        /// Generic return to main menu
        /// </summary>
        public void ReturnToMainMenu() {
            this._state = State.closed;
        }

        /// <summary>
        /// individual user's main menu
        /// </summary>
        public abstract void DisplayUserMenu();

        /// <summary>
        /// begin user transaction
        /// </summary>
        public virtual void Action() {
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
        #endregion

        #region static!!!
        /// <summary>
        /// not sure where else to put this
        /// technically anyone who sees this is a user?
        /// </summary>
        /// <param name="driver"></param>
        public static void DisplayMainMenu(Driver driver, UnitOfWork uow)
        {
            Console.WriteLine("Welcome to Marvelous Magic");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Owner");
            Console.WriteLine("2. Franchise Holder");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Quit");
            Console.WriteLine(" ");
            Console.Write("Enter an option:");
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
                            user = new Owner(uow);
                            break;
                        case "2":
                            user = new FranchiseHolder(uow);
                            break;
                        case "3":
                            user = new Customer(uow);
                            break;
                        case "4":
                            driver.CloseApplication();
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            DisplayMainMenu(driver, uow);
                            break;
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                DisplayMainMenu(driver, uow);
            }
            finally
            {
                //displose of object
                if (user != null)
                {
                    user = null;
                }
            }
        }
        #endregion
    }
}

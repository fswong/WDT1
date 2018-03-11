using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    //this is the main controller
    public class Driver
    {
        #region properties
        //used to flag the status
        private State _state { get; set; }
        #endregion

        #region ctor
        /// <summary>
        /// basic constructor
        /// </summary>
        public Driver() {
            _state = State.open;
            Begin();
        }
        #endregion

        #region methods
        /// <summary>
        /// the main function of the application
        /// </summary>
        public void Begin() {
            do
            {
                try
                {
                    User.DisplayMainMenu(this);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    User.DisplayMainMenu(this);
                }

            } while (_state != State.closed);
        }

        /// <summary>
        /// check if still valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            if (_state == State.open) {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// exit
        /// </summary>
        public void CloseApplication() {
            _state = State.closed;
        }
        #endregion
        
    }
}

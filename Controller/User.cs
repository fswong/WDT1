using System;
using System.Collections.Generic;
using System.Text;
using Repository;

namespace Controller
{
    public abstract class User
    {
        #region enum
        public enum UserType
        {
            Owner = 1,
            FranchiseHolder = 2,
            Customer = 3
        }
        #endregion

        #region properties
        public UserType _userType { get; protected set; }
        #endregion

        #region ctor
        public User(Transaction trn) {}
        #endregion

        #region methods
        /// <summary>
        /// Generic return to main menu
        /// </summary>
        public void ReturnToMainMenu() {

        }
        #endregion
    }
}

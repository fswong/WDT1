using System;
using System.Collections.Generic;
using System.Text;
using Repository;

namespace Controller
{
    public class Customer : User
    {
        #region properties
        public BusinessObject.Store store { get; set; }
        #endregion

        #region ctor
        /// <summary>
        /// generic constructor
        /// </summary>
        public Customer():base() {
        }
        #endregion

        #region method

        #endregion
    }
}

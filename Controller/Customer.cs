using System;
using System.Collections.Generic;
using System.Text;
using Common.Interface;
using Repository;

namespace Controller
{
    public class Customer : User, IStorefront
    {
        #region properties
        public BusinessObject.Store store { get; set; }
        #endregion

        #region ctor
        /// <summary>
        /// generic constructor
        /// </summary>
        public Customer(UnitOfWork uow):base(uow:uow) {
        }
        #endregion

        #region method
        public override void DisplayUserMenu()
        {
            throw new NotImplementedException();
        }

        public void DisplayStoreList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

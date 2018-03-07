using System;
using System.Collections.Generic;
using System.Text;
using Repository;

namespace Controller
{
    public class Customer : User
    {
        #region properties

        #endregion

        #region ctor
        public Customer(Transaction trn):base(trn:trn) {
            _userType = UserType.Customer;
        }
        #endregion
    }
}

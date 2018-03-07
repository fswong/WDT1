using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Owner : User 
    {
        #region properties
        #endregion

        #region ctor
        public Owner(Transaction trn) : base(trn:trn) {
            _userType = UserType.Owner;
        }
        #endregion

        #region methods
        /// <summary>
        /// Returns a list of all stock requests
        /// </summary>
        /// <returns></returns>
        public List<DataObject.Product> DisplayAllStockRequests()
        {
            try
            {

                return new List<DataObject.Product>();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<DataObject.Product> DisplayOwnerInventory()
        {
            try
            {

                return new List<DataObject.Product>();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<DataObject.Product> ResetInventoryItemStock()
        {
            try
            {

                return new List<DataObject.Product>();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
    }
}

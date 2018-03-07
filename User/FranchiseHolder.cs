using System;
using System.Collections.Generic;
using System.Text;
using Repository;

namespace BusinessObject
{
    public class FranchiseHolder : User
    {
        #region properties
        #endregion

        #region ctor
        public FranchiseHolder(Transaction trn) : base(trn:trn){
            _userType = UserType.FranchiseHolder;
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
        public List<DataObject.Product> AddNewInventoryItem(long id)
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
        #endregion
    }
}

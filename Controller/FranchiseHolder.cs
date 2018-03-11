using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using Common.Interface;

namespace Controller
{
    public class FranchiseHolder : User, IStorefront
    {
        #region properties
        public BusinessObject.Store _store { get; set; }
        #endregion

        #region ctor
        public FranchiseHolder() : base(){
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

        /// <summary>
        /// the root franchise holder view
        /// </summary>
        public override void DisplayUserMenu()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// lists the available stores
        /// </summary>
        public void DisplayStoreList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

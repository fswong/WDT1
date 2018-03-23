using BusinessObject;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction
{
    public class Store
    {
        #region properties
        public BusinessObject.Store _store { get; protected set; }
        #endregion

        #region ctor
        // generic constructor
        public Store(int StoreID) {
            try {
                _store = new BusinessObject.Store(StoreID: StoreID);
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion

        #region methods
        public void DisplayStoreProducts() {
            var products = _store.ListStoreInventory();

            // ID Product CurrentStock

            string col1 = "ID";
            string col2 = "Product";
            string col3 = "Current Stock";

            List<string> headers = new List<string>();
            headers.Add("Inventory");
            headers.Add(" ");
            headers.Add(col1.PadRight((int)Padding.id) + col2.PadRight((int)Padding.name) + col3.PadRight((int)Padding.quantity));


        }
        #endregion
    }
}

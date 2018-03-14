﻿using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class Product : BusinessObject
    {
        #region properties
        public DataObject.Product _poco { get; protected set; }
        private ProductRepository _repository;
        #endregion

        #region ctor
        /// <summary>
        /// Product constructor, returns poco of product data object
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="id"></param>
        public Product(UnitOfWork uow, int ProductID):base(uow:uow) {
            _repository = new ProductRepository();
            _poco = _repository.GetProductById(ProductID);
        }
        #endregion

        #region methods
        /// <summary>
        /// if the product has quantity less than 20, set it to 20
        /// </summary>
        /// <param name="id"></param>
        public void ResetInventoryItemStock(int StoreID, int ProductID) {

        }

        /// <summary>
        /// adds a product to the store with default quantity 1
        /// </summary>
        /// <param name="id"></param>
        public void AddNewInventoryItem(long id, long storeId) {

        }
        #endregion
    }
}

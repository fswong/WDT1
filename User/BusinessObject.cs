using System;
using Repository;

namespace BusinessObject
{
    public abstract class BusinessObject
    {
        #region properties
        //one single database transaction
        private Repository.Transaction _transaction;
        #endregion

        #region ctor
        /// <summary>
        /// Generic constructor
        /// </summary>
        /// <param name="trn"></param>
        public BusinessObject(Transaction trn) {
            _transaction = trn;
        }
        #endregion
    }
}

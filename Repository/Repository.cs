using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public abstract class Repository
    {
        #region properties
        protected Transaction _transaction;
        #endregion

        #region ctor
        /// <summary>
        /// Generic constructor
        /// </summary>
        /// <param name="trn"></param>
        public Repository(Transaction trn) {
            _transaction = trn;
        }
        #endregion
    }
}

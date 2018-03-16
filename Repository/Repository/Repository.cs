using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    // this is not required by this project
    public abstract class Repository
    {
        #region properties
        protected UnitOfWork _context;
        #endregion

        #region ctor
        /// <summary>
        /// Generic constructor
        /// </summary>
        /// <param name="uow"></param>
        public Repository(UnitOfWork uow) {
            _context = uow;
        }
        #endregion
    }
}

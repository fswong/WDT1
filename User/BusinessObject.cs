using System;
using Repository;

namespace BusinessObject
{
    public abstract class BusinessObject
    {
        #region properties
        //one single database transaction
        protected Repository.UnitOfWork _context;
        #endregion

        #region ctor
        /// <summary>
        /// Generic constructor
        /// </summary>
        /// <param name="uow"></param>
        public BusinessObject(UnitOfWork uow) {
            _context = uow;
        }
        #endregion
    }
}

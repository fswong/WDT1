using System;
using Repository;

namespace BusinessObject
{
    // initially when i built this i thought there would be more uses for this
    // turns out this is just over engineering for a project of this scale
    public abstract class BusinessObject
    {
        #region properties
        //one single database transaction
        //protected Repository.UnitOfWork _context;
        #endregion

        #region ctor
        /// <summary>
        /// Generic get constructor, used to return a POCO of the business object
        /// </summary>
        /// <param name="uow"></param>
        public BusinessObject(long Id) {
        }
        #endregion
    }
}

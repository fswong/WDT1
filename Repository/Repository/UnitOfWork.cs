using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace Repository
{
    public class UnitOfWork : DbContext
    {
        #region properties
        // DO NOT commit the file with this string
        private const string _connString = "Server=<SERVERADDRESS>;User Id=<USERID>;Password=<PASSWORD>";

        // EF related
        public DbSet<DataObject.OwnerInventory> OwnerInventorySet { get; set; }
        public DbSet<DataObject.Product> ProductSet { get; set; }
        public DbSet<DataObject.StockRequest> StockRequestSet { get; set; }
        public DbSet<DataObject.Store> StoreSet { get; set; }
        public DbSet<DataObject.StoreInventory> StoreInventorySet { get; set; }
        #endregion

        #region ctor
        //public UnitOfWork()
        //{
        //}
        #endregion

        #region override
        /// <summary>
        /// to use connection string in efcore
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }
        #endregion
    }
}

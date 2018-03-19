using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository
{
    // this is technically not required, keeping this class for future reference only
    //public class UnitOfWork : DbContext
    //{
    //    #region properties
    //    // DO NOT commit the file with this string
    //    private const string _connString = "";

    //    // EF related
    //    public DbSet<DataObject.OwnerInventory> OwnerInventorySet { get; set; }
    //    public DbSet<DataObject.Product> ProductSet { get; set; }
    //    public DbSet<DataObject.StockRequest> StockRequestSet { get; set; }
    //    public DbSet<DataObject.Store> StoreSet { get; set; }
    //    public DbSet<DataObject.StoreInventory> StoreInventorySet { get; set; }
    //    #endregion

    //    #region ctor
    //    public UnitOfWork()
    //    {
    //    }
    //    #endregion

    //    #region override
    //    /// <summary>
    //    /// to use connection string in efcore
    //    /// </summary>
    //    /// <param name="optionsBuilder"></param>
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlServer(_connString);
    //    }

    //    /// <summary>
    //    /// fluent api to handle multiple keys
    //    /// https://github.com/aspnet/EntityFrameworkCore/issues/2344
    //    /// </summary>
    //    /// <param name="modelBuilder"></param>
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<DataObject.StoreInventory>()
    //            .HasKey(c => new { c.StoreID, c.ProductID });
    //    }
    //    #endregion
    //}

    public class UnitOfWork : IDisposable
    {
        #region properties
        private const string _connString = "Server=wdt2018.australiaeast.cloudapp.azure.com;Database=s3593297;Uid=s3593297;Pwd=abc123;";
        private SqlConnection _conn;
        private SqlTransaction _transaction;
        #endregion

        #region ctor
        public UnitOfWork() {
            _conn = OpenConnection();
            _transaction = _conn.BeginTransaction();
        }
        #endregion

        #region methods
        public void Close() {
            _transaction.Dispose();
            _conn.Close();
        }

        public void Commit() {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Runquery(string query) {
            using (SqlCommand cmd = new SqlCommand(query, _conn, _transaction))
            {
                cmd.ExecuteScalar();
            }
        }

        public DataTable GetDataTable(string query) {
            using (SqlDataAdapter da = new SqlDataAdapter(query, _conn))
            {
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public string[] Select(string query) {
            string[] output = null;
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    output = new string[reader.FieldCount];
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            output[i] = reader[i].ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion

        /// <summary>
        /// automatically open the connection
        /// </summary>
        /// <returns></returns>
        private SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            return conn;
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository
{
    /// <summary>
    /// factory
    /// </summary>
    static class DBConn
    {
        #region properties
        // DO NOT commit this with the querystring
        //private static string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["main"].ConnectionString;
        //private static IConfigurationRoot Configuration { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //private static string _connString = Configuration["ConnectionString"];

        
        private const string _connString = "Server=wdt2018.australiaeast.cloudapp.azure.com;Database=s3593297;Uid=s3593297;Pwd=abc123;";
        #endregion

        #region methods
        /// <summary>
        /// generic insert
        /// </summary>
        /// <param name="query"></param>
        internal static void Insert(string query) {
            using (SqlConnection conn = OpenConnection()) {
                using (SqlTransaction trn = conn.BeginTransaction()) {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn, trn))
                        {
                            cmd.ExecuteNonQuery();

                            //commit on success
                            trn.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        //rollback
                        Console.WriteLine(e.Message);
                        trn.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// generic update method
        /// </summary>
        /// <param name="query"></param>
        internal static void Update(string query)
        {
            using (SqlConnection conn = OpenConnection())
            {
                using (SqlTransaction trn = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn, trn))
                        {
                            cmd.ExecuteScalar();

                            //commit on success
                            trn.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        //rollback
                        Console.WriteLine(e.Message);
                        trn.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// generic query returns data table
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static DataTable GetDataTable(string query)
        {
            try {
                using (SqlConnection conn = OpenConnection())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        return table;
                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// returns single query result
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static string[] Select(string query)
        {
            try {
                string[] output = null;
                using (SqlConnection conn = OpenConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
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
                }
                return output;
            } catch (Exception) {
                throw;
            }
            
        }

        /// <summary>
        /// automatically open the connection
        /// </summary>
        /// <returns></returns>
        private static SqlConnection OpenConnection()
        {
            //IConfigurationRoot Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //string _connString = Configuration["ConnectionString"];

            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            return conn;
        }
        #endregion
    }
}

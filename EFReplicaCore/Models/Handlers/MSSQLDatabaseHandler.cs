using EFReplicaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace EFReplicaCore.Models.Handlers
{
    public class MSSQLDatabaseHandler : IHandler
    {
        private readonly string conn;

        public MSSQLDatabaseHandler(string conn)
        {
            this.conn = conn;
        }

        public object ExecuteSelect(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection sqlConnection = new SqlConnection(conn);
                ds.Clear();
                using (SqlDataAdapter da = new SqlDataAdapter(query, sqlConnection))
                {
                    da.Fill(ds);
                }
                return ds.Tables[0];
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool ExecuteCommand(string query)
        {
            bool success = true;

            SqlConnection sqlConnection = new SqlConnection(conn);
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                try
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch
                {
                    success = false;
                }
            }

            return success;
        }
    }
}

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DBConnection
    {
        /// <summary>
        /// GetConnection method is used for the open database connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection Conn = null;

            try
            {
                string SqlConn = ConfigurationManager.AppSettings["SqlConn"].ToString();

                Conn = new SqlConnection(SqlConn);

                if(Conn.State==System.Data.ConnectionState.Closed)
                {
                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
            }
            return Conn;
        }

        /// <summary>
        /// CloseConnection this method is used for the close the database connection
        /// </summary>
        /// <param name="SqlConn"></param>
        public static void CloseConnection(SqlConnection SqlConn)
        {
            if (SqlConn.State == System.Data.ConnectionState.Closed)
            {
                SqlConn.Close();
            }
        }
    }
}

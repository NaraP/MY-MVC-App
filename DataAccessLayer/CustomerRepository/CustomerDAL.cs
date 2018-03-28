using BusinessEntities.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility.ErrorLog;

namespace DataAccessLayer.CustomerRepository
{
    public class CustomerDAL : ICustomerDAL
    {
        private IErrorLogger ErrorLog = null;

        public CustomerDAL(IErrorLogger _ErrorLog)
        {
            ErrorLog = _ErrorLog;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objcust"></param>
        /// <returns></returns>
        public string InsertCustomerData(Customer objcust)
        {
            SqlConnection Connection = null;

            string result = "";

            try
            {
                Connection = DBConnection.GetConnection();

                using (SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);

                    cmd.Parameters.AddWithValue("@Name", objcust.Name);

                    cmd.Parameters.AddWithValue("@Address", objcust.Address);

                    cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);

                    cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);

                    cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);

                    cmd.Parameters.AddWithValue("@Query", 1);

                    result = cmd.ExecuteScalar().ToString();
                }
            }

            catch (Exception ex)
            {
                ErrorLog.ExceptionWriteIntoTextFile(ex, "InsertCustomerData", "Data access", "CUstomer module");
            }

            finally
            {
                DBConnection.CloseConnection(Connection);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objcust"></param>
        /// <returns></returns>
        public string UpdateCustomerData(Customer objcust)
        {
            SqlConnection Connection = null;

            string result = "";

            try
            {
                Connection = DBConnection.GetConnection();

                using (SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);

                    cmd.Parameters.AddWithValue("@Name", objcust.Name);

                    cmd.Parameters.AddWithValue("@Address", objcust.Address);

                    cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);

                    cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);

                    cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);

                    cmd.Parameters.AddWithValue("@Query", 2);

                    result = cmd.ExecuteScalar().ToString();
                }
            }

            catch (Exception ex)
            {
                ErrorLog.ExceptionWriteIntoTextFile(ex, "UpdateCustomerData", "Data access", "CUstomer module");
            }

            finally
            {
                DBConnection.CloseConnection(Connection);
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objcust"></param>
        /// <returns></returns>
        public string DeleteCustomerData(Customer objcust)
        {
            SqlConnection Connection = null;

            string result = "";

            try
            {
                Connection = DBConnection.GetConnection();

                using (SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);

                    cmd.Parameters.AddWithValue("@Name", null);

                    cmd.Parameters.AddWithValue("@Address", null);

                    cmd.Parameters.AddWithValue("@Mobileno", null);

                    cmd.Parameters.AddWithValue("@Birthdate", null);

                    cmd.Parameters.AddWithValue("@EmailID", null);

                    cmd.Parameters.AddWithValue("@Query", 3);

                    result = cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionWriteIntoTextFile(ex, "DeleteCustomerData", "Data access", "CUstomer module");
            }

            finally
            {
                DBConnection.CloseConnection(Connection);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerData()
        {
            SqlConnection Connection = null;

            DataTable dt = null;
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                Connection = DBConnection.GetConnection();

                using (SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", "Test101");

                    cmd.Parameters.AddWithValue("@Name", null);

                    cmd.Parameters.AddWithValue("@Address", null);

                    cmd.Parameters.AddWithValue("@Mobileno", null);

                    cmd.Parameters.AddWithValue("@Birthdate", null);

                    cmd.Parameters.AddWithValue("@EmailID", null);

                    cmd.Parameters.AddWithValue("@Query", 4);

                    da.SelectCommand = cmd;

                    dt = new DataTable();

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionWriteIntoTextFile(ex, "GetCustomerData", "Data access", "CUstomer module");
            }

            finally
            {
                DBConnection.CloseConnection(Connection);
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DataTable SelectCustomerDatabyID(string CustomerID)
        {
            SqlConnection Connection = null;

            SqlDataAdapter da = new SqlDataAdapter();

            List<Customer> _Customer = new List<Customer>();

            DataTable dt = new DataTable();

            try
            {
                Connection = DBConnection.GetConnection();

                using (SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                    cmd.Parameters.AddWithValue("@Name", null);

                    cmd.Parameters.AddWithValue("@Address", null);

                    cmd.Parameters.AddWithValue("@Mobileno", null);

                    cmd.Parameters.AddWithValue("@Birthdate", null);

                    cmd.Parameters.AddWithValue("@EmailID", null);

                    cmd.Parameters.AddWithValue("@Query", 5);

                    da.SelectCommand = cmd;

                    da.Fill(dt);

                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    cobj = new Customer();

                    //    cobj.CustomerID = Convert.ToString(ds.Tables[0].Rows[i]["CustomerID"].ToString());

                    //    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();

                    //    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();

                    //    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();

                    //    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();

                    //    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());
                    //}
                }
            }
            

            catch (Exception ex)
            {
                ErrorLog.ExceptionWriteIntoTextFile(ex, "SelectCustomerDatabyID", "Data access", "CUstomer module");
            }

            finally
            {
                DBConnection.CloseConnection(Connection);
            }
            return dt;
        }
    }
}

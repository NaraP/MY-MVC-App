using BusinessEntities.Customers;
using DataAccessLayer.CustomerRepository;
using System;
using System.Collections.Generic;
using System.Data;
using Utility.ErrorLog;

namespace BusinessLogicLayer.CustomerRepository
{
    public class CustomerBLL : ICustomerBLL
    {
        public ICustomerDAL CustDAL = null;

        private IErrorLogger ErrorLog = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustDal"></param>
        public CustomerBLL(ICustomerDAL CustDal, IErrorLogger _ErrorLog)
        {
            CustDAL = CustDal;
            ErrorLog = _ErrorLog;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objcust"></param>
        /// <returns></returns>
        public string InserCustomerData(Customer objcust)
        {
            return CustDAL.InsertCustomerData(objcust);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objcust"></param>
        /// <returns></returns>
        public string UpdateCustomerData(Customer objcust)
        {
            return CustDAL.UpdateCustomerData(objcust);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objcust"></param>
        /// <returns></returns>
        public string DeleteCustomerData(Customer objcust)
        {
            return CustDAL.DeleteCustomerData(objcust);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomerData()
        {
            List<Customer> custlist = new List<Customer>();

            DataTable dtCust = new DataTable();

            Customer cobj = new Customer();

            try
            {
                dtCust = CustDAL.GetCustomerData();

                //We can use below query 
                //custlist = (from DataRow dr in dtCust.Rows
                //              select new Customer()
                //              {
                //                  CountryCode = Convert.ToString(dr["COUNTRY_CODE"]),
                //                  CountryName = Convert.ToString(dr["COUNTRY_DESC"].ToString())
                //              }).ToList();

                foreach (DataRow dr in dtCust.Rows)
                {
                    cobj.CustomerID = Convert.ToString(dr["CustomerID"].ToString());

                    cobj.Name = dr["Name"].ToString();

                    cobj.Address = dr["Address"].ToString();

                    cobj.Mobileno = dr["Mobileno"].ToString();

                    cobj.EmailID = dr["EmailID"].ToString();

                    cobj.Birthdate = Convert.ToDateTime(dr["Birthdate"].ToString());

                    custlist.Add(cobj);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionWriteIntoTextFile(ex, "GetCustomerData", "Business layer", "CUstomer module");
            }
            return custlist;
        }

        /// <summary>
        /// SelectCustomerDatabyID method is used for the select data based on the ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DataTable SelectCustomerDatabyID(string CustomerID)
        {
            return CustDAL.SelectCustomerDatabyID(CustomerID);
        }
    }
}

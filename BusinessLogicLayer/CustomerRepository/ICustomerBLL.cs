using BusinessEntities.Customers;
using System.Collections.Generic;
using System.Data;

namespace BusinessLogicLayer.CustomerRepository
{
    public  interface ICustomerBLL
    {
        string InserCustomerData(Customer objcust);

        string UpdateCustomerData(Customer objcust);

        string DeleteCustomerData(Customer objcust);

        List<Customer> GetCustomerData();

        DataTable SelectCustomerDatabyID(string CustomerID);
    }
}

using BusinessEntities.Customers;
using System.Data;

namespace DataAccessLayer.CustomerRepository
{
    public interface ICustomerDAL
    {
        string InsertCustomerData(Customer objcust);
        string UpdateCustomerData(Customer objcust);
        string DeleteCustomerData(Customer objcust);
        DataTable GetCustomerData();
        DataTable SelectCustomerDatabyID(string CustomerID);
    }
}

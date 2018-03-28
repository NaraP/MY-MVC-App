using System;

namespace BusinessEntities.Customers
{
    public  class Customer
    {
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobileno { get; set; }
        public DateTime Birthdate { get; set; }
        public string EmailID { get; set; }
    }
}

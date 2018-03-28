using BusinessLogicLayer.CustomerRepository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCDIWebApplication.Controllers.Customer
{
    public class CustomerController : Controller
    {
        private  ICustomerBLL CustomerRepository;
        public CustomerController(ICustomerBLL _CustBLL)
        {
            CustomerRepository = _CustBLL;
        }
        // GET: Customer
        [ExceptionLoggerFilter]
        public ActionResult Index()
        {
            List<BusinessEntities.Customers.Customer> Customers = CustomerRepository.GetCustomerData();

            // We can use here Auto mapper to map the source object an destination object
            MVCDIWebApplication.Models.Customer _cust = null;

            List<MVCDIWebApplication.Models.Customer> CustList = new List<Models.Customer>();

            foreach (var cust in Customers)
            {
                _cust = new MVCDIWebApplication.Models.Customer();

                _cust.CustomerID = cust.CustomerID;
                _cust.Name = cust.Name;
                _cust.Mobileno = cust.Mobileno;
                _cust.Birthdate = cust.Birthdate;
                _cust.EmailID = cust.EmailID;
                CustList.Add(_cust);
            }
            return View(CustList);
        }

        //
        // GET: /Customer/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        [ExceptionLoggerFilter]
        public ActionResult Create(MVCDIWebApplication.Models.Customer Customer)
        {
            BusinessEntities.Customers.Customer Customers = new BusinessEntities.Customers.Customer();

            try
            {
                // TODO: Add insert logic here
                Customers.CustomerID = Customer.CustomerID;
                Customers.Name = Customer.Name;
                Customers.Address = Customer.Address;
                Customers.Mobileno = Customer.Mobileno;
                Customers.Birthdate = Customer.Birthdate;
                Customers.EmailID = Customer.EmailID;

                CustomerRepository.InserCustomerData(Customers);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(string CustomerID)
        {
            MVCDIWebApplication.Models.Customer objCustomer = new Models.Customer();

            return View(CustomerRepository.SelectCustomerDatabyID(CustomerID));
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ExceptionLoggerFilter]
        public ActionResult Edit(MVCDIWebApplication.Models.Customer objCustomer)
        {
            BusinessEntities.Customers.Customer Customers = new BusinessEntities.Customers.Customer();

            try
            {
                // TODO: Add update logic here

                objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);

                if (ModelState.IsValid) //checking model is valid or not
                {
                    Customers.CustomerID = objCustomer.CustomerID;
                    Customers.Name = objCustomer.Name;
                    Customers.Address = objCustomer.Address;
                    Customers.Mobileno = objCustomer.Mobileno;
                    Customers.Birthdate = objCustomer.Birthdate;
                    Customers.EmailID = objCustomer.EmailID;
                    string result = CustomerRepository.UpdateCustomerData(Customers);

                    ViewData["result"] = result;

                    ModelState.Clear(); //clearing model
                }
                else
                {
                    ModelState.AddModelError("", "Error in saving data");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(string ID)
        {
            return View(CustomerRepository.SelectCustomerDatabyID(ID));
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost]
        [ExceptionLoggerFilter]
        public ActionResult Delete(MVCDIWebApplication.Models.Customer objCustomer)
        {
            BusinessEntities.Customers.Customer Customers = new BusinessEntities.Customers.Customer();

            Customers.CustomerID = objCustomer.CustomerID;

            try
            {
                // TODO: Add delete logic here

                string result = CustomerRepository.DeleteCustomerData(Customers);

                ViewData["result"] = result;

                ModelState.Clear(); //clearing model
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
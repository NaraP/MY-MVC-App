using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using BusinessLogicLayer.CustomerRepository;

namespace MVCDIWebApplication.Controllers
{
    public class HomeController : Controller
    {
       //private  ICustomerBLL CustomerRepository;
       // public HomeController(ICustomerBLL repository)
       // {
       //     this.CustomerRepository = repository;
       // }

        public ActionResult Index()
        {
            //CustomerRepository.GetCustomerData();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
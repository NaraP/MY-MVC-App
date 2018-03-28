using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDIWebApplication.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Customer ID")]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Mobileno")]
        public string Mobileno { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Enter EmailID")]
        public string EmailID { get; set; }
    }
}
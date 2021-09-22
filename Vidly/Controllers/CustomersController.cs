﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View(GetCustommers());
        }

        //[Route("customers/details")]
        //public ActionResult Details()
        //{
        //    return View(GetCustommers().ToList()[0]);
        //}

        [Route("customers/details/{id}")]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                id = 1;
            }
            var customer = GetCustommers().SingleOrDefault(c => c.Id == id);

            return View(customer);
        }

        

        public IEnumerable<Customer> GetCustommers()
        {
            var customers = new List<Customer>()
            {
                new Customer() {Name = "Exequiel Arroyo", Id = 123213},
                new Customer() {Name = "Christine Arroyo", Id = 4353},
            };

            return customers;
        }
    }
}
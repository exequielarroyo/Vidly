using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

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

            customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return customers;
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerViewModel()
            {
                Customer = new Customer(),  
                MembershipTypes = membershipTypes
            };
            return View("CustomerFormViewModel",viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerFormViewModel", viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Birthdate = customer.Birthdate;
            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerViewModel() {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerFormViewModel", viewModel);
        }
    }
}
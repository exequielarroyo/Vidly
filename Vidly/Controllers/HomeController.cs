using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //passing data to View
            var movie = new Movie() { Name = "Shrek!" };
            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;

            var customers = new List<Customer>()
            {
                new Customer(){ Id=5432, Name="Exequiel"},
                new Customer(){ Id=6543, Name="Christine"},
                new Customer(){ Id=6543, Name="Christine"},
                new Customer(){ Id=6543, Name="Christine"},
                new Customer(){ Id=6543, Name="Christine"},
                new Customer(){ Id=6543, Name="Christine"}
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };
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
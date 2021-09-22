using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            //passing data to View
            var movie = new MovieModel() { Name = "Shrek!" };
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

            return View(viewModel);
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });

        }

        //GET: Movies or movies
        [Route("movies/{movieId:range(1, 100)}")]
        public ActionResult Edit(int movieId)
        {
            return Content("ID: " + movieId);
        }

        [Route("movies/{pageIndex:regex(\\d{3})}/{sortBy}")]
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (String.IsNullOrEmpty(sortBy))
            {
                sortBy = "Name";
            }

            return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        }

        public ActionResult Index()
        {
            var movies = new List<MovieModel>()
            {
                new MovieModel() { Name = "Brave Heart" },
                new MovieModel() { Name = "The day after tomorrow" },
            };

            return View(movies);
        }
    }
}
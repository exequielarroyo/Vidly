using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
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

            return View(viewModel);
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });

        }

        //GET: Movies or movies
        //[Route("movies/{movieId:range(1, 100)}")]
        //public ActionResult Edit(int movieId)
        //{
        //    return Content("ID: " + movieId);
        //}

        //[Route("movies/{pageIndex:regex(\\d{3})}/{sortBy}")]
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (String.IsNullOrEmpty(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        //}

        public ActionResult Index()
        {
            return View(GetMovies());
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = GetMovies().SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

        private ApplicationDbContext context;

        public MoviesController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            context.Dispose();
        }

        public IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie() { Name = "Brave Heart", Id = 1 },
                new Movie() { Name = "The day after tomorrow", Id = 2 },
            };

            movies = context.Movies.Include(m => m.Genre).ToList();

            return movies;
        }

        public ActionResult Edit(int id)
        {
            var movie = context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = context.Genres.ToList()
            };

            return View("MovieFormViewModel", viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new MovieViewModel
            {
                Genres = context.Genres.ToList()
            };

            return View("MovieFormViewModel", viewModel);
        }

        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.DateAdded = movieInDb.DateAdded;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Stock = movie.Stock;
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}
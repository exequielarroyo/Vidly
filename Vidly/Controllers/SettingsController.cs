using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var model = new MovieModel();
            var i = 5 / model.Id;
            return View();
        }

        public ActionResult Movies()
        {
            List<MovieModel> movies = new List<MovieModel>();
            movies.Add(new MovieModel { Name = "Squid Game", Id = 4109873 });
            movies.Add(new MovieModel { Name = "Tournament", Id = 3237823 });
            movies.Add(new MovieModel { Name = "Tomorrow", Id = 5154873 });

            return View(movies);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalController : ApiController
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult Create(NewRentalDto newRental)
        {
            if (newRental.MoviesId.Count == 0)
                return BadRequest("No movies has been given.");

            var customerInDb = context.Customers.SingleOrDefault(r => r.Id == newRental.CustomerId);
            var movies = context.Movies.Where(m => newRental.MoviesId.Contains(m.Id)).ToList();

            if (customerInDb == null)
                return BadRequest("Customer id is not valid.");
            if (movies.Count != newRental.MoviesId.Count)
                return BadRequest("One or more movies are invalid.");



            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customerInDb,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                context.Rentals.Add(rental);
            }

            context.SaveChanges();

            return Ok();
        }
    }
}

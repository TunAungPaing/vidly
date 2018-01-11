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
        private ApplicationDbContext _Context;
        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()    
        {
            var movies = _Context.Movies.Include(c=>c.Genre).ToList();
            
            return View(movies);
        }

        public ActionResult MovieDetail(int id)
        {
            var movie = _Context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.id == id);
            
            return View(movie);

        }

        public ActionResult MovieForm()
        {
            var GenreName = _Context.Genres.ToList();
            var viewModel = new MovieViewModel
            {
                Movie = new Movie(),
                Genres = GenreName
        };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieViewModel
                {
                    Movie = movie,
                    Genres = _Context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.id==0)
            {
                movie.DateAdded = DateTime.Now;
                _Context.Movies.Add(movie);
            }
            else
            {
                var movieindb = _Context.Movies.SingleOrDefault(m => m.id == movie.id);
                movieindb.Name = movie.Name;
                movieindb.ReleaseDate = movie.ReleaseDate;
                movieindb.DateAdded = DateTime.Now;
                movieindb.GenreId = movie.GenreId;
                movieindb.NumberInStock = movie.NumberInStock;
            }
            
            _Context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = _Context.Genres.ToList()
            };
            return View("MovieForm",viewModel);
        }
        
    }
}
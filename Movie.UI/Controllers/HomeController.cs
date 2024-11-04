using Microsoft.AspNetCore.Mvc;
using Movie.UI.Models;
using System.Diagnostics;

namespace Movie.UI.Controllers
{
    public class MovieEntity
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }

    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly List<MovieEntity> _movies = new List<MovieEntity>
        {
            new MovieEntity { Title = "Inception", Year = 2010, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SX300.jpg"},
            new MovieEntity { Title = "The Godfather", Year = 1972, ImageUrl = "https://m.media-amazon.com/images/M/MV5BYTJkNGQyZDgtZDQ0NC00MDM0LWEzZWQtYzUzZDEwMDljZWNjXkEyXkFqcGc@._V1_SX300.jpg" },
            new MovieEntity { Title = "Pulp Fiction", Year = 1994, ImageUrl = "https://m.media-amazon.com/images/M/MV5BYTViYTE3ZGQtNDBlMC00ZTAyLTkyODMtZGRiZDg0MjA2YThkXkEyXkFqcGc@._V1_SX300.jpg" }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string searchText = null, int? year = null)
        {
            var filteredMovies = _movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredMovies = filteredMovies.Where(m => m.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            }

            if (year.HasValue)
            {
                filteredMovies = filteredMovies.Where(m => m.Year == year.Value);
            }

            return View(filteredMovies.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
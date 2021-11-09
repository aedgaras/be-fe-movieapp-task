using Microsoft.AspNetCore.Mvc;
using movieapp.API.DataAccess;
using movieapp.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public MoviesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("{name?}")]
        public async Task<IEnumerable<Movie>> GetMoviesAsync(string name)
        {
            // If query is empty return all table members.
            if (name == null)
                return _databaseContext.Movies.ToList();

            // If query isn't empty return members which contain the query.
            var dbMovies = _databaseContext.Movies.Where(movie => movie.Title.Contains(name) == true).ToList();
            if (dbMovies.Count > 0)
                return dbMovies;

            // If there's no members with the query in their title send request to external API and save the results to local MSSQL database.
            var client = new RestClient($"http://www.omdbapi.com/?s={name}&apikey=e19f2d81");
            var request = new RestRequest(Method.GET);
            var response = client.ExecuteAsync(request).Result;

            var moviesResponse = JsonConvert.DeserializeObject<MovieListResponse>(response.Content);
            // If response failed return database members.
            if (moviesResponse.Response == "False")
                return _databaseContext.Movies.ToList();

            // Add movies from API response to database, whilist checking for dublication.
            foreach (var movie in moviesResponse.Search)
                if (_databaseContext.Movies.Where(x => x.ImdbId == movie.ImdbId).ToList().Count == 0)
                    _databaseContext.Movies.Add(movie);

            _databaseContext.SaveChanges();

            return _databaseContext.Movies.ToList();
        }

        [HttpGet("movie/{id?}")]
        public async Task<MovieDetails> GetMovieByIdAsync(string id)
        {
            // If query is empty return first member from local database.
            if (string.IsNullOrEmpty(id))
                return _databaseContext.MoviesDetails.FirstOrDefault();

            // If movie that matches the query exists return it.
            var movieDetails = _databaseContext.MoviesDetails.FirstOrDefault(x => x.ImdbId == id);
            if (movieDetails != null)
                return movieDetails;

            // Movie doesn't exist in local database, make request to API and save it.
            var client = new RestClient($"http://www.omdbapi.com/?i={id}&apikey=e19f2d81");
            var request = new RestRequest(Method.GET);
            var response = client.ExecuteAsync(request).Result;

            var serielizer = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var movie = JsonConvert.DeserializeObject<MovieResponse>(response.Content, serielizer);

            // If request failed return null.
            if (movie.Response == "False")
                return null;

            // Add movie actors to database.
            var actorsString = movie.Actors.Split(", ");
            foreach (var actorname in actorsString)
            {
                var actor = new Actor { Name = actorname };
                if (_databaseContext.Actors.Where(x => x.Name == actorname).ToList().Count == 0)
                {
                    _databaseContext.Actors.Add(actor);
                }
            }

            // Add movie genres to database.
            var genresString = movie.Genre.Split(", ");
            foreach (var genreName in genresString)
            {
                var genre = new Genre { Name = genreName };
                if (_databaseContext.Genres.Where(x => x.Name == genreName).ToList().Count == 0)
                {
                    _databaseContext.Genres.Add(genre);
                }
            }

            // Create Movie object which will be saved and returned.
            var movieObject = new MovieDetails
            {
                ImdbId = movie.imdbID,
                Title = movie.Title,
                ReleaseDate = movie.Year,
                Actors = movie.Actors,
                Genre = movie.Genre,
                ImdbRating = double.Parse(movie.imdbRating),
                About = movie.Plot
            };

            if (_databaseContext.MoviesDetails.Where(x => x.ImdbId == movie.imdbID).ToList().Count == 0)
            {
                _databaseContext.MoviesDetails.Add(movieObject);
            }

            _databaseContext.SaveChanges();

            return movieObject;
        }
    }
}

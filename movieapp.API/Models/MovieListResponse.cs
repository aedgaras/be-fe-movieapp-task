using System.Collections.Generic;

namespace movieapp.API.Models
{
    public class MovieListResponse
    {
        public IEnumerable<Movie> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }
    }

}

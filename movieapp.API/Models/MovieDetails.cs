using System.Collections.Generic;

namespace movieapp.API.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public double ImdbRating { get; set; }
        public string About { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }

    }
}

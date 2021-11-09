using System.ComponentModel.DataAnnotations;

namespace movieapp.API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
    }
}

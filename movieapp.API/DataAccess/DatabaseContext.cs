using Microsoft.EntityFrameworkCore;
using movieapp.API.Models;

namespace movieapp.API.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDetails> MoviesDetails { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}

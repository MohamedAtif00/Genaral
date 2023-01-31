using Microsoft.EntityFrameworkCore;
using MoviesSites.Models.JunctionClasses;
using System.Reflection.Emit;
using MoviesSites.Models.ViewModel;

namespace MoviesSites.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Movie> movies { get; set; }
        public DbSet<Actor> actors { get; set; }
        public DbSet<ActorMovie> actorMovies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                    .HasKey(am => new { am.ActorId, am.MovieId });

            modelBuilder.Entity<ActorMovie>()
                    .HasOne(am => am.Actor)
                    .WithMany(a => a.Movies)
                    .HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<ActorMovie>()
                    .HasOne(am => am.Movie)
                    .WithMany(m => m.ActorsMovies)
                    .HasForeignKey(am => am.MovieId);

        }


        public DbSet<MoviesSites.Models.ViewModel.MovieViewModel> MovieViewModel { get; set; }
    }
}

using MoviesSites.Models.JunctionClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSites.Models.ViewModel
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<ActorMovie>? Actors { get; set; }

        public MovieType Type { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public enum MovieType
        {
            Action,
            Adventure,
            Comedy,
            Drama,
            Horror,
            Romance
        }
    }

}

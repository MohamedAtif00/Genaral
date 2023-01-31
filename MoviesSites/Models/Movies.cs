using MoviesSites.Models.JunctionClasses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSites.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [EnumDataType(typeof(MovieType))]
        public MovieType Type { get; set; }
        public List<ActorMovie> ActorsMovies { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
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

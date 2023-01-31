using MoviesSites.Models.JunctionClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSites.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ActorMovie> Movies { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }

}

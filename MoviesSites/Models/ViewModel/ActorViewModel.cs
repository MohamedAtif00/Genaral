using MoviesSites.Models.JunctionClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSites.Models.ViewModel
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }      
        [NotMapped]
        public IFormFile Image { get; set; }
        
    }
}

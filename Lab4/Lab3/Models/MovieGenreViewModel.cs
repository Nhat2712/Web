using Microsoft.AspNetCore.Mvc.Rendering;
namespace Lab3.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }
        
        public SelectList? Genres { get; set; }

        public SelectList? Categories { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}

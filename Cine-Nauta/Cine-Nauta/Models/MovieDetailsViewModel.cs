using Cine_Nauta.DAL.Entities;

namespace Cine_Nauta.Models
{
    public class MovieDetailsViewModel : Movie
    {
        public Movie Movie { get; set; }
        public List<Function> Functions { get; set; }
    }
}

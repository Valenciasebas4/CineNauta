using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Classification : EntityCine
    {
        [Display(Name = "Clasificación")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string ClassificationName { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(400)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }

        //public List<Movie> Movies { get; set; } //Relacion con Movie (pelicula)
    }
}

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Gender : EntityCine
    {
        [Display(Name = "Genero")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string GenderName { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }

        //public List<Movie> Movies { get; set; } //Relacion con Movie (pelicula)
    }
}

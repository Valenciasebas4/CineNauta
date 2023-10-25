using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Function : EntityCine
    {
        [Display(Name = "Pelicula")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        [Display(Name = "Sala")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [Display(Name = "Fecha Función")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime FunctionDate { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Price { get; set; }  

    }
}

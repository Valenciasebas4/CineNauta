using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Function : EntityCine
    {
       

        [Display(Name = "Fecha Función")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime FunctionDate { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Price { get; set; }

        [Display(Name = "Pelicula")]
        [JsonIgnore]
        public Movie Movie { get; set; } //Relacion con Pelicula

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RoomId { get; set; }
        public Room Room { get; set; } //Relacion con Sala

        // Una funcion puede tener varias reservas por el mismo o otro usuario
        public ICollection<Reservation> Reservations { get; set; }



    }
}

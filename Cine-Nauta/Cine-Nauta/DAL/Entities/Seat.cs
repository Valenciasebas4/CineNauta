using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Seat : EntityCine
    {
        [Display(Name = "Fila")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Row { get; set; }


        [Display(Name = "Número de Asiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NumberSeat { get; set; }

        [Display(Name = "Estado")]
        public bool Busy { get; set; }

        [Display(Name = "Sala.")]
        [JsonIgnore]
        public virtual Room Room { get; set; } //Relacion con Room(sala)
    }
}

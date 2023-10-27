using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Reservation : EntityCine
    {
        [Display(Name = "Fecha de reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime ReservationDate { get; set; } // Fecha y hora de la reserva

        [Display(Name = "Precio Total")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal TotalPrice { get; set; } // Precio total de la reserva

        // Relación con la función (una reserva pertenece a una función)
        [Display(Name = "Función")]
        public Function Function { get; set; }

        // Relación con el usuario (una reserva pertenece a un usuario)
        [Display(Name = "Usuarios")]
        public User User { get; set; }

        // Asientos reservados (una reserva puede tener múltiples asientos)
        [Display(Name = "Asientos")]
        public ICollection<Seat> Seats { get; set; }
    }
}
